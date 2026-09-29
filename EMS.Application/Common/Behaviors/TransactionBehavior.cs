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
            if (request is not ICommandBase)
                return await next(ct);

            if (uow.HasActiveTransaction)
            {
                return await next(ct);

            }


            await using var tx = await uow.BeginTransactionAsync(ct);
            try
            {
                var response = await next(ct);

                var saved = await uow.SaveChangesAsync(ct);

                await tx.CommitAsync(ct);

                return response;
            }
            catch(Exception)
            {

                await tx.RollbackAsync(ct);
                throw;
            }
        }
    }
}