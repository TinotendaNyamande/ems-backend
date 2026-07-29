namespace EMS.Infrastructure.RabbitMQServices
{
    public interface IRabbitMqPublisher
    {
        Task PublishAsync<T>(T message,string exchange, string routingKey,CancellationToken cancellationToken=default);
    }
}