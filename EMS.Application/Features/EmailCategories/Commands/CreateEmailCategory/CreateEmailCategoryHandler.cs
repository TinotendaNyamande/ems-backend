using AutoMapper;
using EMS.Application.Interfaces;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Commands.CreateEmailCategory
{
    public class CreateEmailCategoryHandler(IEmailCategoryRepository emailCategoryRepository,IMapper mapper,IOrganisationRepository organisationRepository) : IRequestHandler<CreateEmailCategoryCommand>
    {
        public async Task Handle(CreateEmailCategoryCommand request, CancellationToken cancellationToken)
        {
            await organisationRepository.GetByIdAsync(request.OrganisationId);
            var emailCategory = mapper.Map<EmailCategory>(request);
            await emailCategoryRepository.CreateEmailCategoryAsync(emailCategory);
        }
    }
}