using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategories;
using EMS.Domain.Models;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Queries.GetCategoryByName
{
    public record GetCategoryByNameQuery(Guid EmailAccountId,string CategoryName) : ICommand<GetEmailCategoryDto>
    {
    }
}