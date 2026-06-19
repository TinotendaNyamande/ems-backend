using EMS.Application.Dtos.EmailAccounts;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Queries.GetEmailAccount
{
    public class GetEmailAccountQuery(Guid id):IRequest<EmailAccountDto>
    {
        public Guid Id { get; init; } = id;
    }
}
