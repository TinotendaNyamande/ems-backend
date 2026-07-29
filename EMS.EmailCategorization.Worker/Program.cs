using EMS.EmailCategorization.Worker;
using EMS.Infrastructure.Extensions;


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddRabbitMQ(builder.Configuration);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
