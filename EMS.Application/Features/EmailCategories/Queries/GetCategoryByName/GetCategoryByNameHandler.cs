using AutoMapper;
using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategories;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Queries.GetCategoryByName
{
    public class GetCategoryByNameHandler(IEmailCategoryRepository emailCategoryRepository, IMapper mapper) : ICommandHandler<GetCategoryByNameQuery, GetEmailCategoryDto>
    {

        public async Task<GetEmailCategoryDto> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var category = await emailCategoryRepository.GetCategoryByNameAsync(request.EmailAccountId,request.CategoryName);
            return mapper.Map<GetEmailCategoryDto>(category);
        }
    }


}
