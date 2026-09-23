using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.Emails.Commands.AssignEmailCategory
{
    public class AssignEmailCategoryHandler(IEmailRepository emailRepository) : IRequestHandler<AssignEmailCategoryCommand>
    {
        public async Task Handle(AssignEmailCategoryCommand request, CancellationToken cancellationToken)
        {
            await emailRepository.AssignEmailCategoryAsync(request.EmailId,request.CategoryId);
        }
    }
}