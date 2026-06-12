using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Commands.DeleteEmailCategory
{
    public class DeleteEmailCategoryHandler(IEmailCategoryRepository emailCategoryRepository,IEmailInboxRepository emailInboxRepository) : IRequestHandler<DeleteEmailCategoryCommand>
    {
        public async Task Handle(DeleteEmailCategoryCommand request, CancellationToken cancellationToken)
        {
            if(request.NewCategoryId is null)
            {
                await emailCategoryRepository.DeleteEmailCategoryAsync(request.Id);
            }else
            {
                await emailCategoryRepository.GetCategoryByIdAsync(request.NewCategoryId!.Value);
                await emailInboxRepository.ChangeCategoryForBulkEmails(request.Id,request.NewCategoryId!.Value);
                await emailCategoryRepository.DeleteEmailCategoryAsync(request.Id);
            }
            
        }
    }
}