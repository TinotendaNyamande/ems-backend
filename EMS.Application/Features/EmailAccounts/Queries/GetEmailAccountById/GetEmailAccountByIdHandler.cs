using AutoMapper;
using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Queries.GetEmailAccountById
{
    public class GetEmailAccountHandler(IEmailAccountRepository emailAccountRepository,IMapper mapper) : IRequestHandler<GetEmailAccountByIdQuery, EmailAccountDto>
    {
        public async Task<EmailAccountDto> Handle(GetEmailAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var emailAccount = await emailAccountRepository.GetEmailAccountAsync(request.Id);
            return mapper.Map<EmailAccountDto>(emailAccount);
        }
    }
}
