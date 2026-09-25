namespace EMS.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<ITransaction> BeginTransactionAsync(CancellationToken ct);
        Task<int> SaveChangesAsync(CancellationToken ct);
        bool HasActiveTransaction { get; }


    }

}