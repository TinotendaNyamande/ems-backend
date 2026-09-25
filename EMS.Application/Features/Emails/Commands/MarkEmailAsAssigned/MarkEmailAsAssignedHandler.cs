using EMS.Application.Abstractions;
using EMS.Application.Interfaces;
using FluentValidation;
using MediatR;

namespace EMS.Application.Features.Emails.Commands.MarkEmailAsAssigned
{
    internal class MarkEmailAsAssignedHandler(IEmailRepository emailRepository) : ICommandHandler<MarkEmailAsAssignedCommand>
    {
        public async Task Handle(MarkEmailAsAssignedCommand request, CancellationToken cancellationToken)
        {
           await emailRepository.MarkEmailAsAssignedAsync(request.EmailId);
        }
    }
}