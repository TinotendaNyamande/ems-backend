using EMS.EmailReader;
using EMS.Infrastructure.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration,builder.Environment);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
