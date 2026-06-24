using EMS.Application.Interfaces;
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
            var emailProcessingService = scope.ServiceProvider.GetRequiredService<IEmailProcessingService>();
           // await emailProcessingService.EmailReaderService();
           // await emailProcessingService.EmailCategoryService();
            await emailProcessingService.TaskAssignmentService();
            //var emailCategorizer = scope.ServiceProvider.GetRequiredService<IEmailCategorizer>();
            // var categories = new[]
            //         {
            //             "Billing",
            //             "Support",
            //             "Sales",
            //             "Spam",
            //             "Technical Issue",
            //             "Other"
            //         };
            // await emailCategorizer.CategorizeAsync("PC problems", "The computer i purchased is not connecting to the internet", categories);



            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
