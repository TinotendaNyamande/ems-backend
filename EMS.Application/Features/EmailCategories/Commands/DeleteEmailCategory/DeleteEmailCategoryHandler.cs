using EMS.Application.Abstractions;
using EMS.Application.Features.EmailCategories.Queries.GetEmailCategoryById;
using EMS.Application.Features.Emails.Commands.ChangeEmailCategory;
using EMS.Application.Features.Emails.Queries.GetEmailsByCategoryId;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Commands.DeleteEmailCategory
{
    public class DeleteEmailCategoryHandler(IEmailCategoryRepository emailCategoryRepository, IMediator mediator, IUnitOfWork unitOfWork) : ICommandHandler<DeleteEmailCategoryCommand>
    {
        public async Task Handle(DeleteEmailCategoryCommand request, CancellationToken cancellationToken)
        {
            await mediator.Send(new GetEmailCategoryByIdQuery(request.NewCategoryId), cancellationToken);
            var affectedEmails = await mediator.Send(new GetEmailsByCategoryIdQuery(request.CategoryId),cancellationToken);
            foreach(var email in affectedEmails)
            {
                await mediator.Send(new ChangeEmailCategoryCommand(email.Id,request.NewCategoryId),cancellationToken);
            }
            await emailCategoryRepository.DeleteEmailCategoryAsync(request.CategoryId);


        }
    }
}