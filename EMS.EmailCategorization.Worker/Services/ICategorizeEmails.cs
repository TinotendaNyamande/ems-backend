using EMS.Contracts.Events.Email;

namespace EMS.EmailCategorization.Worker.Services
{
    public interface ICategorizeEmails
    {
        Task ProcessAsync(EmailReceivedEvent message, CancellationToken cancellationToken);

    }
}