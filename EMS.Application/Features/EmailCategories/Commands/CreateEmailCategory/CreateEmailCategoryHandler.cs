using AutoMapper;
using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategories;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using EMS.Domain.Exceptions;

namespace EMS.Application.Features.EmailCategories.Commands.CreateEmailCategory
{
    public class CreateEmailCategoryHandler(IEmailCategoryRepository emailCategoryRepository,IEmailAccountRepository emailAccountRepository, IMapper mapper) : ICommandHandler<CreateEmailCategoryCommand, GetEmailCategoryDto>
    {
        public async Task<GetEmailCategoryDto> Handle(CreateEmailCategoryCommand request, CancellationToken cancellationToken)
        {
            var emailAccount = await emailAccountRepository.GetEmailAccountByIdAsync(request.EmailAccountId)
            ?? throw new ResourceNotFoundException("Email Account", request.EmailAccountId);
            var emailCategoryExists = await emailCategoryRepository.GetCategoryByNameAsync(request.EmailAccountId, request.CategoryName);
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