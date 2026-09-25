using AutoMapper;
using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Queries.GetEmailAccountById
{
    public class GetEmailAccountByIdHandler(IEmailAccountRepository emailAccountRepository,IMapper mapper) : ICommandHandler<GetEmailAccountByIdQuery, EmailAccountDto>
    {
        public async Task<EmailAccountDto> Handle(GetEmailAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var emailAccount = await emailAccountRepository.GetEmailAccountByIdAsync(request.Id);
            return mapper.Map<EmailAccountDto>(emailAccount);
        }
    }
}
