using EMS.API.Extensions;
using EMS.API.Middleware;
using EMS.Application.Extensions;
using EMS.Infrastructure.Extensions;
using EMS.Infrastructure.Seeder;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLogging();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration,builder.Environment)
    .AddPresentation(builder.Configuration);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seeder = services.GetRequiredService<IRoleSeeder>();
    await seeder.SeedAsync(services);
}

// Configure the HTTP request pipeline.

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
