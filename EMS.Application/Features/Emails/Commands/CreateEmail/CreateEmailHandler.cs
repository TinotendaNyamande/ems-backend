using AutoMapper;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.Emails.Commands.CreateEmail
{
    internal class CreateEmailHandler(IMapper mapper,IEmailRepository emailRepository) : IRequestHandler<CreateEmailCommand, Email>
    {
        public async Task<Email> Handle(CreateEmailCommand request, CancellationToken cancellationToken)
        {
            var email = mapper.Map<Email>(request);
            var savedEmail = await emailRepository.CreateEmailAsync(email);
            return savedEmail;
        }
    }
}