namespace EMS.EmailReader.Worker.Services

{
    public interface IEmailReaderProcessor
    {
        Task ProcessAsync(CancellationToken cancellationToken);
    }
}
