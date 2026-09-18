using EMS.Infrastructure.Extensions;
using EMS.TaskAssignment.Worker;
using EMS.TaskAssignment.Worker.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration,builder.Environment);
builder.Services.AddRabbitMQ(builder.Configuration);
builder.Services.AddScoped<ITaskAssignmentService,TaskAssignmentService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
