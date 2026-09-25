using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Common.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse>(IUnitOfWork uow)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken ct)
        {
            var ctxId = (uow as IUnitOfWork)?.GetHashCode();  // or expose ContextId
            Console.WriteLine($"--> {typeof(TRequest).Name} | HasActive={uow.HasActiveTransaction} | ctx={ctxId}");
            if (request is not ICommandBase)
                return await next();

            if (uow.HasActiveTransaction)
            {
                Console.WriteLine($"    short-circuit (already in tx) for {typeof(TRequest).Name}");
                return await next();

            }

            Console.WriteLine($"    BEGIN tx for {typeof(TRequest).Name}");

            await using var tx = await uow.BeginTransactionAsync(ct);
            try
            {
                var response = await next();

                var saved = await uow.SaveChangesAsync(ct);
                Console.WriteLine($"    SaveChanges returned {saved} for {typeof(TRequest).Name}");

                await tx.CommitAsync(ct);
                Console.WriteLine($"    COMMIT for {typeof(TRequest).Name}");

                return response;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"    ROLLBACK for {typeof(TRequest).Name}: {ex.Message}");

                await tx.RollbackAsync(ct);
                throw;
            }
        }
    }
}