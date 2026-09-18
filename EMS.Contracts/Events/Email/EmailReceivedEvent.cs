using EMS.Contracts.Common;

namespace EMS.Contracts.Events.Email
{
    public record EmailReceivedEvent : EventBase
    {
        public Guid EmailId { get; init; }
        public string Subject { get; init; } = string.Empty;
        public string Body { get; init; } = string.Empty;
        public string Sender { get; init; } = string.Empty;
        public DateTime ReceivedAt { get; init; }
        public Guid EmailAccountId { get; init; }
    }
}