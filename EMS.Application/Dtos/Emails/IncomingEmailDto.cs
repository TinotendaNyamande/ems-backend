namespace EMS.Application.Dtos.Emails
{
    public record IncomingEmailDto(string FromEmail, string ToEmail, string Subject, string Body, string ExternalMessageId) ;

}