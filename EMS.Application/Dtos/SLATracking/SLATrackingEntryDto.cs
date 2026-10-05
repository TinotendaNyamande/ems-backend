using EMS.Domain.Enums;

namespace EMS.Application.Dtos.SLATracking
{
    public record SLATrackingEntryDto(Guid Id, DateTime StartTime, DateTime? EndTime, string Comments);

}