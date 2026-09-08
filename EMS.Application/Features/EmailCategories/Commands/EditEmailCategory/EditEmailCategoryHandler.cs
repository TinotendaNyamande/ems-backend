using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Commands.EditEmailCategory
{
    public class EditEmailCategoryHandler(IEmailCategoryRepository emailCategoryRepository) : IRequestHandler<EditEmailCategoryCommand>
    {
        public async Task Handle(EditEmailCategoryCommand request, CancellationToken cancellationToken)
        {
            await emailCategoryRepository.EditEmailCategoryAsync(request.Id, request.NewName, request.SlaHours);
        }
    }
}