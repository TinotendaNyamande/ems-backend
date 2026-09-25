namespace EMS.Application.Dtos.Emails
{
    public record CategorizeEmailDto(Guid EmailId,Guid EmailAccountId, string Subject,string Body);
}