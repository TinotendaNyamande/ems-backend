using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace EMS.Infrastructure.RabbitMQServices
{
    public sealed class RabbitMqConnection(IOptions<RabbitMqOptions> options) : IRabbitMqConnection
    {
        private IConnection? _connection;
        private readonly RabbitMqOptions _options = options.Value;
        private readonly SemaphoreSlim _lock = new(1, 1);

        public async Task<IConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
        {
            if (_connection is { IsOpen: true })
            {
                return _connection;
            }
            if (_connection is not null)
            {
                await _connection.DisposeAsync();
            }
            await _lock.WaitAsync(cancellationToken);
            try
            {
                if (_connection is { IsOpen: true })
                {
                    return _connection;
                }
                var factory = new ConnectionFactory
                {
                    HostName = _options.Host,
                    Port = _options.Port,
                    UserName = _options.Username,
                    Password = _options.Password
                };
                _connection = await factory.CreateConnectionAsync();
                return _connection;
            }
            finally
            {
                _lock.Release();
            }
        }
        public async ValueTask DisposeAsync()
        {
            if (_connection is not null)
            {
                await _connection.DisposeAsync();
            }
            _lock.Dispose();
        }
    }
}