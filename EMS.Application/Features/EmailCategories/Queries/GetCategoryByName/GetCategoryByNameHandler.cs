using AutoMapper;
using EMS.Application.Dtos.EmailCategories;
using EMS.Application.Interfaces;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Queries.GetCategoryByName
{
    public class GetCategoryByNameHandler(IEmailCategoryRepository emailCategoryRepository, IMapper mapper) : IRequestHandler<GetCategoryByNameQuery, GetEmailCategoryDto>
    {
        private readonly IEmailCategoryRepository _emailCategoryRepository = emailCategoryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<GetEmailCategoryDto> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var category = await _emailCategoryRepository.GetCategoryByNameAsync(request.CategoryName);
            return _mapper.Map<GetEmailCategoryDto>(category);
        }
    }


}
