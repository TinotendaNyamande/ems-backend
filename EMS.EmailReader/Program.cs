using EMS.EmailReader;
using EMS.EmailReader.Services;
using EMS.Infrastructure.Extensions;
using Microsoft.Extensions.AI;
using System.ClientModel;
using OpenAI;
using EMS.EmailReader.Services.TaskAssignment;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<IPipelineStep, EmailReaderProcessor>();
builder.Services.AddScoped<IPipelineStep, EmailCategoryProcessor>();
builder.Services.AddScoped<IPipelineStep, TaskAssignmentProcessor>();
var fireworksApiKey = builder.Configuration["Fireworks:APIKey"];
var fireworksModelId = builder.Configuration["Fireworks:ModelId"]
    ?? "accounts/fireworks/models/deepseek-v4-flash";
var fireworksEndpoint = builder.Configuration["Fireworks:Endpoint"]
    ?? "https://api.fireworks.ai/inference/v1";

builder.Services.AddChatClient(_ =>
{
    if (string.IsNullOrWhiteSpace(fireworksApiKey))
    {
        throw new InvalidOperationException(
            "Missing Fireworks API key. Set Fireworks:APIKey in configuration or FIREWORKS_API_KEY as an environment variable.");
    }

    var openAIClient = new OpenAIClient(
        new ApiKeyCredential(fireworksApiKey),
        new OpenAIClientOptions
        {
            Endpoint = new Uri(fireworksEndpoint)
        });

    return openAIClient.GetChatClient(fireworksModelId).AsIChatClient();
});

var host = builder.Build();
host.Run();
