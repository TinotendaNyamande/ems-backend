using EMS.Application.Abstractions;
using EMS.Application.Features.AuditTrail.Commands.CreateAuditTrailEntry;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.Emails.Commands.ChangeEmailCategory
{
    internal class ChangeEmailCategoryHandler(IEmailRepository emailRepository,IMediator mediator) : ICommandHandler<ChangeEmailCategoryCommand>
    {
        public async Task Handle(ChangeEmailCategoryCommand request, CancellationToken cancellationToken)
        {
            await emailRepository.ChangeEmailCategoryAsync(request.EmailId, request.NewCategoryId);
        }
    }
}