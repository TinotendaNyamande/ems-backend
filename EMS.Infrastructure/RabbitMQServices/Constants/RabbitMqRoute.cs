namespace EMS.Infrastructure.RabbitMQServices.Constants
{
    public sealed record RabbitMqRoute
    {
        public required string Exchange { get; init; }
        public required string RoutingKey { get; init; }
        public string ExchangeType { get; init; } = RabbitMQ.Client.ExchangeType.Direct;
        public required string Queue {get;init;}
    }
}