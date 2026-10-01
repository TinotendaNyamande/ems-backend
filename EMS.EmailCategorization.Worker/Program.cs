using EMS.Application.Extensions;
using EMS.EmailCategorization.Worker;
using EMS.EmailCategorization.Worker.Services;
using EMS.Infrastructure.Extensions;
using EMS.Infrastructure.RabbitMQServices.Constants;


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration,builder.Environment);
builder.Services.AddApplication();
builder.Services.AddRabbitMQ(builder.Configuration);
builder.Services.AddSingleton(RabbitMqRoutes.EmailReceived);
builder.Services.AddScoped<IEmailCategorizerProcess,EmailCategorizerProcess>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
