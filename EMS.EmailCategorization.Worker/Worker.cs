using EMS.EmailCategorization.Worker.Services;

namespace EMS.EmailCategorization.Worker;

public class Worker(IServiceScopeFactory scopeFactory,ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                using var scope = scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IEmailEventReader>();
                await service.ReadEmailsPendingCategorizationAsync(stoppingToken);
                logger.LogInformation("Worker finished processing at: {time}", DateTimeOffset.Now);
                
            }
            await Task.Delay(10000, stoppingToken);
        }
    }
}
