using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategories;

namespace EMS.Application.Features.EmailCategories.Commands.CreateEmailCategory
{
    public record CreateEmailCategoryCommand(Guid EmailAccountId, string CategoryName,double SLAHours):ICommand<GetEmailCategoryDto>;
}