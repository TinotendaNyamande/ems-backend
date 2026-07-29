namespace EMS.EmailCategorization.Worker.Services
{
    public interface IEmailCategorizerService
    {
        Task<int> ProcessAsync(CancellationToken cancellationToken);
    }
}