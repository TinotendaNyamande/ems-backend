namespace EMS.EmailReader.Services
{
    public interface IPipelineStep
    {
        Task<int> ProcessAsync(CancellationToken cancellationToken);
    }
}