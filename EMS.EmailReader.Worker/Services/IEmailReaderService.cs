namespace EMS.EmailReader.Worker.Services

{
    public interface IEmailReaderService
    {
        Task<int> ProcessAsync(CancellationToken cancellationToken);
    }
}
