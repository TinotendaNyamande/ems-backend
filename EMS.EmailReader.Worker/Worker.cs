using EMS.EmailReader.Worker.Services;

namespace EMS.EmailReader.Worker;

public class Worker(IServiceScopeFactory scopeFactory, ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {

                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                using var scope = scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IEmailReaderProcessor>();
                await service.ProcessAsync(stoppingToken);
                logger.LogInformation("Worker finished processing at: {time}", DateTimeOffset.Now);

            
            await Task.Delay(3000, stoppingToken);
        }
    }
}
