using EMS.Contracts.Events.Email;
using EMS.EmailCategorization.Worker.Services;
using EMS.Infrastructure.RabbitMQServices;
using EMS.Infrastructure.RabbitMQServices.Constants;

namespace EMS.EmailCategorization.Worker.Consumers
{
    public sealed class EmailReceivedConsumer(IRabbitMqConnection _connection, ILogger logger, RabbitMqRoute route,IEmailCategorizerService service) : RabbitMqConsumer<EmailReceivedEvent>(_connection, logger, route)
    {
        protected override async Task HandleAsync(EmailReceivedEvent message,CancellationToken token)
        {
            await service.ProcessAsync(message,token);
        }
    }
}