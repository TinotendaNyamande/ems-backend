using EMS.Application.Interfaces;

namespace EMS.Infrastructure.Repository
{
    internal sealed class LocalFileStorage : IFileStorageRepository
{
    public Task<Stream> OpenReadAsync(string path, CancellationToken ct = default)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException("Attachment file not found", path);

        Stream stream = new FileStream(
            path,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 4096,
            useAsync: true);

        return Task.FromResult(stream);
    }

    public Task<bool> ExistsAsync(string path, CancellationToken ct = default)
        => Task.FromResult(File.Exists(path));
}
}