using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Commands.RenameEmailCategory
{
    public class RenameEmailCategoryHandler(IEmailCategoryRepository emailCategoryRepository) : IRequestHandler<RenameEmailCategoryCommand>
    {
        public async Task Handle(RenameEmailCategoryCommand request, CancellationToken cancellationToken)
        {
            await emailCategoryRepository.RenameEmailCategoryAsync(request.Id,request.NewName);
        }
    }
}