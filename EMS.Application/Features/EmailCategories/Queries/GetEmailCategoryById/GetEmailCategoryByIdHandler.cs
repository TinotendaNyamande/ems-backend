using AutoMapper;
using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategories;
using EMS.Application.Interfaces;

namespace EMS.Application.Features.EmailCategories.Queries.GetEmailCategoryById
{
    public class GetEmailsByCategoryIdHandler(IEmailCategoryRepository emailCategoryRepository,IMapper mapper) : IQueryHandler<GetEmailCategoryByIdQuery, GetEmailCategoryDto>
    {
        public async Task<GetEmailCategoryDto> Handle(GetEmailCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var emailCategory = await emailCategoryRepository.GetCategoryByIdAsync(request.Id);
            return mapper.Map<GetEmailCategoryDto>(emailCategory);
        }
    }
}