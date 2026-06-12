namespace EMS.EmailReader.Services
{
    public interface IEmailProcessingService
    {
        Task ReadEmailsFromInbox();
        Task DetermineEmailCategory();
        Task AssignEmailToUser();
    }
}