using EMS.Contracts.Common;

namespace EMS.Contracts.Events.Notifications
{
    public record EmailReceivedEvent:EventBase
    {
        public Guid EmailId { get; init; }
        public string Subject { get; init; } = string.Empty;
        public string EmailCategory { get; init; } = string.Empty;
        public string AssignedToEmail { get; init; } = string.Empty;
        public string AssignedToDisplayName { get; init; } = string.Empty;
        public Guid TaskId { get; init; }
    }
}