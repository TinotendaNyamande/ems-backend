using EMS.Application.Dtos.EmailCategories;
using MediatR;

namespace EMS.Application.Features.EmailCategories.Commands.CreateEmailCategory
{
    public record CreateEmailCategoryCommand(Guid EmailAccountId, string CategoryName,double SLAHours):IRequest<GetEmailCategoryDto>;
}