using AutoMapper;
using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailAccounts;
using EMS.Application.Features.EmailAccounts.Queries.GetEmailAccountById;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailAccounts.Commands.TestEmailAccount
{
    internal class TestEmailHandler(IEmailSender emailSender,IMediator mediator,IMapper mapper,IEncryptionService encryptionService): ICommandHandler<TestEmailAccountCommand>
    {
        public async Task Handle(TestEmailAccountCommand request, CancellationToken cancellationToken)
        {
            var emailAccount = await mediator.Send(new GetEmailAccountByIdQuery(request.EmailAccountId),cancellationToken);
            var emailAccountDto = mapper.Map<ValidationAndTestEmailAccountDto>(emailAccount);
            if(emailAccount.EmailType==Domain.Enums.EmailType.Office365)
            {
                emailAccountDto.ClientSecret = encryptionService.DecryptData(emailAccount.ClientSecret);
            }
            else
            {
                emailAccountDto.Password = encryptionService.DecryptData(emailAccount.Password);
            }
            await emailSender.SendTestEmailAsync(emailAccountDto, request.ToEmail);
        }
    }
}
