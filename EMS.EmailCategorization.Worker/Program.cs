using EMS.EmailCategorization.Worker;
using EMS.EmailCategorization.Worker.Consumers;
using EMS.Infrastructure.Extensions;
using EMS.Infrastructure.RabbitMQServices;


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddRabbitMQ(builder.Configuration);

builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService<EmailReceivedConsumer>();

var host = builder.Build();
host.Run();
