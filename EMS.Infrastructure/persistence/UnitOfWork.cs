using EMS.Application.Interfaces;
using EMS.Infrastructure.persistence;

namespace EMS.Infrastructure.persistence
{
    internal sealed class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
        public async Task<ITransaction> BeginTransactionAsync(CancellationToken ct)
        {
            var efTransaction = await context.Database.BeginTransactionAsync(ct);
            return new Transaction(efTransaction);
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return await context.SaveChangesAsync(ct);
        }
        public bool HasActiveTransaction => context.Database.CurrentTransaction is not null;

    }
}