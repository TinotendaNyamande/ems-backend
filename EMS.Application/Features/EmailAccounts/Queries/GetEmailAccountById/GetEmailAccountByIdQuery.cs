using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailAccounts;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Queries.GetEmailAccountById
{
    public class GetEmailAccountByIdQuery(Guid id):ICommand<EmailAccountDto>
    {
        public Guid Id { get; init; } = id;
    }
}
