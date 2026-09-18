namespace EMS.EmailCategorization.Worker.Services
{
    public interface IEmailEventReader
    {
        Task ReadEmailsPendingCategorizationAsync(CancellationToken cancellationToken);
    }
}