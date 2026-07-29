namespace EMS.Contracts.Common
{
    public abstract record EventBase
    {
    public Guid EventId { get; init; } = Guid.NewGuid();

    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;

    public string CorrelationId { get; init; } = Guid.NewGuid().ToString();

    public string Source { get; init; } = string.Empty;
    }
}