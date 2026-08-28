using System.Text;
using System.Text.Json;
using EMS.Contracts.Common;
using EMS.Infrastructure.RabbitMQServices.Constants;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;

namespace EMS.Infrastructure.RabbitMQServices
{
    public abstract class RabbitMqConsumer<T>(IRabbitMqConnection _connection, ILogger<RabbitMqConsumer<T>> logger, RabbitMqRoute route) : BackgroundService where T : EventBase
    {
        protected abstract Task HandleAsync(T message, CancellationToken cancellationToken);
        protected override async Task ExecuteAsync(CancellationToken token)
        {
            var connection = await _connection.GetConnectionAsync(token);
            await using var channel = await connection.CreateChannelAsync(cancellationToken: token);
            await channel.ExchangeDeclareAsync(
                exchange: route.Exchange,
                type: route.ExchangeType,
                durable: true,
                autoDelete: false,
                cancellationToken: token
            );
            await channel.QueueDeclareAsync(
                queue: route.Queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                cancellationToken: token
            );
            await channel.QueueBindAsync(
                queue: route.Queue,
                exchange: route.Exchange,
                routingKey: route.RoutingKey,
                cancellationToken: token
            );
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (sender, args) =>
            {

                try
                {
                    var json = Encoding.UTF8.GetString(args.Body.Span);

                    var message = JsonSerializer.Deserialize<T>(json);
                    if (message == null)
                    {
                        logger.LogError(
                            "Failed to deserialize message of type {MessageType}",
                            typeof(T).Name);

                        await channel.BasicNackAsync(
                            args.DeliveryTag,
                            false,
                            false,
                            token);

                        return;
                    }
                    await HandleAsync(message, token);
                    await channel.BasicAckAsync(args.DeliveryTag, false, token);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Processing failed");

                    await channel.BasicNackAsync(
                        args.DeliveryTag,
                        false,
                        true,
                        token);
                }


            };
            await channel.BasicConsumeAsync(
               queue: route.Queue,
               autoAck: false,
               consumerTag: string.Empty,
               noLocal: false,
               exclusive: false,
               arguments: null,
               consumer: consumer,
               cancellationToken: token);
            await Task.Delay(Timeout.Infinite, token);
        }
    }
}