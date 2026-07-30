using System.Text;
using System.Text.Json;
using EMS.Contracts.Common;
using EMS.Infrastructure.RabbitMQServices.Constants;
using RabbitMQ.Client;

namespace EMS.Infrastructure.RabbitMQServices
{
    public class RabbitMqPublisher(IRabbitMqConnection connection) : IRabbitMqPublisher
    {
        private readonly IRabbitMqConnection _connection = connection;

        public async Task PublishAsync<T>(T message, RabbitMqRoute route, CancellationToken cancellationToken = default) where T:EventBase
        {
            var connection = await _connection.GetConnectionAsync(cancellationToken);
            var channel = await connection.CreateChannelAsync(cancellationToken:cancellationToken);
            await channel.ExchangeDeclareAsync(
                exchange:route.Exchange,
                type:route.ExchangeType,
                durable:true,
                autoDelete:false,
                cancellationToken:cancellationToken
            );
            var body = JsonSerializer.SerializeToUtf8Bytes(message);
            var properties = new BasicProperties
            {
                Persistent=true,
                ContentType="application/json",
                ContentEncoding="utf-8",
                MessageId=message.EventId.ToString(),
                Timestamp= new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds()),
                CorrelationId = message.CorrelationId
            };
            await channel.BasicPublishAsync(
                exchange:route.Exchange,
                routingKey:route.RoutingKey,
                mandatory:true,
                basicProperties:properties,
                body:body,
                cancellationToken:cancellationToken
            );
        }
    }
}