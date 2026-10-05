using EMS.Application.Abstractions;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.Emails.Commands.ChangeEmailCategory
{
    internal class ChangeEmailCategoryHandler(IEmailRepository emailRepository) : ICommandHandler<ChangeEmailCategoryCommand>
    {
        public async Task Handle(ChangeEmailCategoryCommand request, CancellationToken cancellationToken)
        {
            await emailRepository.ChangeEmailCategoryAsync(request.EmailId, request.NewCategoryId);
        }
    }
}