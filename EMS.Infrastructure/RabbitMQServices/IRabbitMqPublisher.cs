using EMS.Contracts.Common;
using EMS.Infrastructure.RabbitMQServices.Constants;

namespace EMS.Infrastructure.RabbitMQServices
{
    public interface IRabbitMqPublisher
    {
        Task PublishAsync<T>(T message,RabbitMqRoute route,CancellationToken cancellationToken=default) where T:EventBase;
    }
}