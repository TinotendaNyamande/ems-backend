using EMS.Application.Dtos.EmailAccounts;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Queries.GetEmailAccountById
{
    public class GetEmailAccountByIdQuery(Guid id):IRequest<EmailAccountDto>
    {
        public Guid Id { get; init; } = id;
    }
}
