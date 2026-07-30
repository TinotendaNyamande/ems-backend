using RabbitMQ.Client;

namespace EMS.Infrastructure.RabbitMQServices.Constants;

public static class RabbitMqRoutes
{
    public static readonly RabbitMqRoute EmailReceived =
        new()
        {
            Exchange = "emails.exchange",
            RoutingKey = "email.received",
            Queue = "email.received",
            ExchangeType = ExchangeType.Direct
        };

    public static readonly RabbitMqRoute EmailCategorized =
        new()
        {
            Exchange = "emails.exchange",
            RoutingKey = "email.categorized",
            Queue="email.categorized",
            ExchangeType = ExchangeType.Direct
        };

    public static readonly RabbitMqRoute EmailAssigned =
        new()
        {
            Exchange = "emails.exchange",
            RoutingKey = "email.assigned",
            Queue = "email.assigned",
            ExchangeType = ExchangeType.Direct
        };
}