namespace EMS.EmailCategorization.Worker.Services
{
    public interface IEmailCategorizerProcess
    {
        Task ReadEmailsPendingCategorizationAsync(CancellationToken cancellationToken);
    }
}