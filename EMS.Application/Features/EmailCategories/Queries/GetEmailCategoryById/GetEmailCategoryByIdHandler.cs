using AutoMapper;
using EMS.Application.Dtos.EmailCategories;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Queries.GetEmailCategoryById
{
    public class GetEmailCategoryByIdHandler(IEmailCategoryRepository emailCategoryRepository,IMapper mapper) : IRequestHandler<GetEmailCategoryByIdQuery, GetEmailCategoriesDto>
    {
        public async Task<GetEmailCategoriesDto> Handle(GetEmailCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var emailCategory = await emailCategoryRepository.GetCategoryByIdAsync(request.Id);
            return mapper.Map<GetEmailCategoriesDto>(emailCategory);
        }
    }
}