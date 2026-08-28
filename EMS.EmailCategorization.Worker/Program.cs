using EMS.EmailCategorization.Worker;
using EMS.EmailCategorization.Worker.Consumers;
using EMS.EmailCategorization.Worker.Services;
using EMS.Infrastructure.Extensions;
using EMS.Infrastructure.RabbitMQServices.Constants;
using Microsoft.Extensions.AI;
using OpenAI;
using System.ClientModel;


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration,builder.Environment);
builder.Services.AddRabbitMQ(builder.Configuration);
builder.Services.AddSingleton(RabbitMqRoutes.EmailReceived);
builder.Services.AddScoped<IEmailCategorizerService,EmailCategorizerService>();

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

builder.Services.AddHostedService<EmailReceivedConsumer>();

var host = builder.Build();
host.Run();
