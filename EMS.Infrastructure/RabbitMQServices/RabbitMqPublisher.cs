using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace EMS.Infrastructure.RabbitMQServices
{
    public class RabbitMqPublisher(IRabbitMqConnection connection) : IRabbitMqPublisher
    {
        private readonly IRabbitMqConnection _connection = connection;

        public async Task PublishAsync<T>(T message, string exchange, string routingKey, CancellationToken cancellationToken = default)
        {
            var connection = await _connection.GetConnectionAsync(cancellationToken);
            var channel = await connection.CreateChannelAsync(cancellationToken:cancellationToken);
            await channel.ExchangeDeclareAsync(
                exchange:exchange,
                type:ExchangeType.Direct,
                durable:true,
                autoDelete:false,
                cancellationToken:cancellationToken
            );
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);
            var properties = new BasicProperties
            {
                Persistent=true,
                ContentType="application/json",
                ContentEncoding="utf-8",
                MessageId=Guid.NewGuid().ToString(),
                Timestamp= new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            };
            await channel.BasicPublishAsync(
                exchange:exchange,
                routingKey:routingKey,
                mandatory:true,
                basicProperties:properties,
                body:body,
                cancellationToken:cancellationToken
            );
        }
    }
}