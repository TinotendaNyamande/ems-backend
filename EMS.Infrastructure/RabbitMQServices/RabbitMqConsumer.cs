using EMS.Contracts.Common;
using EMS.Infrastructure.RabbitMQServices.Constants;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EMS.Infrastructure.RabbitMQServices
{
    public abstract class RabbitMqConsumer<T>(IRabbitMqConnection _connection, ILogger logger,RabbitMqRoute route) : BackgroundService where T : EventBase
    {
        protected override async Task ExecuteAsync(CancellationToken token)
        {
            var connection = await _connection.GetConnectionAsync(token);
            await using var channel = await connection.CreateChannelAsync(cancellationToken:token);
            await channel.ExchangeDeclareAsync(
                exchange:route.Exchange,
                type:route.ExchangeType,
                durable:true,
                autoDelete:false,
                cancellationToken:token
            );
            await channel.QueueDeclareAsync(
                queue:route.Queue,
                durable:true,
                exclusive:false,
                autoDelete:false,
                cancellationToken:token
            );
        }
    }
}