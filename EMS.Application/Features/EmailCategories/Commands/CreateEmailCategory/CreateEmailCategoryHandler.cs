using AutoMapper;
using EMS.Application.Dtos.EmailCategories;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Commands.CreateEmailCategory
{
    public class CreateEmailCategoryHandler(IEmailCategoryRepository emailCategoryRepository,IMapper mapper,IEmailAccountRepository emailAccountRepository) : IRequestHandler<CreateEmailCategoryCommand, GetEmailCategoryDto>
    {
        public async Task<GetEmailCategoryDto> Handle(CreateEmailCategoryCommand request, CancellationToken cancellationToken)
        {
            await emailAccountRepository.GetEmailAccountAsync(request.EmailAccountId);
            var emailCategoryExists = await emailCategoryRepository.EmailCategoryExistsInEmailAccountAsync(request.EmailAccountId, request.CategoryName);
            if (emailCategoryExists)
            {
                throw new InvalidOperationException($"Email category with name '{request.CategoryName}' already exists in this email account.");
            }

            var emailCategory = mapper.Map<EmailCategory>(request);
            var emailCategoryResult = await emailCategoryRepository.CreateEmailCategoryAsync(emailCategory);
            return mapper.Map<GetEmailCategoryDto>(emailCategoryResult);
        }
    }
}