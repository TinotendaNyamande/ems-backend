using AutoMapper;
using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategories;
using EMS.Application.Features.EmailAccounts.Queries.GetEmailAccountById;
using EMS.Application.Features.EmailCategories.Queries.GetCategoryByName;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Commands.CreateEmailCategory
{
    public class CreateEmailCategoryHandler(IEmailCategoryRepository emailCategoryRepository,IMapper mapper,IMediator mediator) : ICommandHandler<CreateEmailCategoryCommand, GetEmailCategoryDto>
    {
        public async Task<GetEmailCategoryDto> Handle(CreateEmailCategoryCommand request, CancellationToken cancellationToken)
        {
            await mediator.Send(new GetEmailAccountByIdQuery(request.EmailAccountId),cancellationToken);
            var emailCategoryExists = await mediator.Send(new GetCategoryByNameQuery(request.EmailAccountId,request.CategoryName),cancellationToken);
            if (emailCategoryExists != null)
            {
                throw new InvalidOperationException($"Email category with name '{request.CategoryName}' already exists in this email account.");
            }

            var emailCategory = mapper.Map<EmailCategory>(request);
            var emailCategoryResult = await emailCategoryRepository.CreateEmailCategoryAsync(emailCategory);
            return mapper.Map<GetEmailCategoryDto>(emailCategoryResult);
        }
    }
}