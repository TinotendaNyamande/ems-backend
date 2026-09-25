using EMS.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace EMS.Infrastructure.persistence
{
    internal sealed class Transaction : ITransaction
    {
        private readonly IDbContextTransaction _transaction;
        private bool _completed;
        public Transaction(IDbContextTransaction transaction)
              => _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        public async Task CommitAsync(CancellationToken ct = default)
        {
            if (_completed) return;               
            await _transaction.CommitAsync(ct);
            _completed = true;
        }

        public async Task RollbackAsync(CancellationToken ct = default)
        {
            if (_completed) return;               
            await _transaction.RollbackAsync(ct);
            _completed = true;
        }
        public async ValueTask DisposeAsync()
        {
            // If the caller forgot to commit, disposing an uncommitted EF transaction
            // rolls it back automatically — which is what we want.
            await _transaction.DisposeAsync();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}