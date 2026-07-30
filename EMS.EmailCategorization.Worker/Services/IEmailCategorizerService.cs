using EMS.Contracts.Events.Email;

namespace EMS.EmailCategorization.Worker.Services
{
    public interface IEmailCategorizerService
    {
        Task<int> ProcessAsync(EmailReceivedEvent message, CancellationToken cancellationToken);
    }
}