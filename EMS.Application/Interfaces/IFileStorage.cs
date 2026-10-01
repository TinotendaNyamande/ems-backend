namespace EMS.Application.Interfaces
{
    public interface IFileStorageRepository
    {
        Task<Stream> OpenReadAsync(string path, CancellationToken ct = default);
        Task<bool> ExistsAsync(string path, CancellationToken ct = default);
    }
}