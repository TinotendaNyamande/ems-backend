using EMS.EmailReader.Services;

namespace EMS.EmailReader;

public class Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            using var scope = scopeFactory.CreateScope();
            var steps = scope.ServiceProvider.GetServices<IPipelineStep>();
            try
            {
                foreach (var step in steps)
                {
                    try
                    {
                        logger.LogInformation("Executing pipeline step: {StepName}", step.GetType().Name);

                        var count =await step.ProcessAsync(stoppingToken);

                        logger.LogInformation("Finished pipeline step: {StepName}", step.GetType().Name);
                        logger.LogInformation("Processed {count} items in pipeline step: {StepName}", count, step.GetType().Name);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex,
                            "Pipeline step {StepName} failed.",
                            step.GetType().Name);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while executing pipeline step: {stepName}", ex.Message);
            }
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
