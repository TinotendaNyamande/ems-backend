namespace EMS.Domain.Enums
{
    public static class RabbitMqConstants
    {
        public const string Exchange = "emails.exchange";
        public static class Queues
        {
            public const string EmailReceived = "email.received";
            public const string EmailCategorized = "email.categorized";
            public const string EmailAssigned = "email.assigned";
        }
        public static class RoutingKeys
        {
            public const string EmailReceived = "email.received";
            public const string EmailCategorized = "email.categorized";
            public const string EmailAssigned = "email.assigned";
        }

    }
}