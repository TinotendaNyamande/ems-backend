using EMS.Infrastructure.Extensions;
using EMS.TaskAssignment.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddRabbitMQ(builder.Configuration);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
