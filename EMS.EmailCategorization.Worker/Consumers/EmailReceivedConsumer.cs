using EMS.Contracts.Events.Email;
using EMS.EmailCategorization.Worker.Services;
using EMS.Infrastructure.RabbitMQServices;
using EMS.Infrastructure.RabbitMQServices.Constants;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.EmailCategorization.Worker.Consumers
{
    public sealed class EmailReceivedConsumer(
        IRabbitMqConnection _connection,
        ILogger<EmailReceivedConsumer> logger,
        RabbitMqRoute route,
        IServiceScopeFactory scopeFactory) : RabbitMqConsumer<EmailReceivedEvent>(_connection, logger, route)
    {
        protected override async Task HandleAsync(EmailReceivedEvent message, CancellationToken token)
        {
            using var scope = scopeFactory.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ICategorizeEmails>();
            await service.ProcessAsync(message, token);
        }
    }
}
