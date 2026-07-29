using RabbitMQ.Client;

namespace EMS.Infrastructure.RabbitMQServices;

public interface IRabbitMqConnection : IAsyncDisposable
{
    Task<IConnection> GetConnectionAsync (CancellationToken cancellationToken=default);
}