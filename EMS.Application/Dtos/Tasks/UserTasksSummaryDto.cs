namespace EMS.Application.Dtos.Tasks
{
    public record UserTasksSummaryDto (int TotalTasks,int OnHold,int Open, int Closed,double? AverageResolutionTime){};
}