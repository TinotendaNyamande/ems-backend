using EMS.Application.Abstractions;
using EMS.Application.Features.Emails.Commands.ChangeEmailCategory;
using EMS.Application.Interfaces;
using EMS.Domain.Exceptions;

namespace EMS.Application.Features.EmailCategories.Commands.DeleteEmailCategory
{
    public class DeleteEmailCategoryHandler(IEmailCategoryRepository emailCategoryRepository,IEmailRepository emailRepository) : ICommandHandler<DeleteEmailCategoryCommand>
    {
        public async Task Handle(DeleteEmailCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = await emailCategoryRepository.GetCategoryByIdAsync(request.NewCategoryId)??
            throw new ResourceNotFoundException("New Email Category", request.NewCategoryId);
            var affectedEmails = await emailRepository.GetEmailsByCategoryIdAsync(request.CategoryId);
            foreach(var email in affectedEmails)
            {
                await emailRepository.ChangeEmailCategoryAsync(email.Id, request.NewCategoryId);
            }
            await emailCategoryRepository.DeleteEmailCategoryAsync(request.CategoryId);


        }
    }
}