using EMS.Domain.Enums;

namespace EMS.Application.Dtos.SLATracking
{
    public record UpdateSLAEntryDto(Guid Id, DateTime? EndTime, string Comments, SLAEntryStatus Status);
  
}