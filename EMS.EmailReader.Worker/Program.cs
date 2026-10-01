using EMS.Application.Extensions;
using EMS.EmailReader.Worker;
using EMS.EmailReader.Worker.Services;
using EMS.Infrastructure.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration,builder.Environment);
builder.Services.AddApplication();
builder.Services.AddRabbitMQ(builder.Configuration);
builder.Services.AddScoped<IEmailReaderProcessor,EmailReaderProcessor>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
