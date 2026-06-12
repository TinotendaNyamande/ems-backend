using MediatR;

namespace EMS.Application.Features.EmailCategories.Commands.CreateEmailCategory
{
    public record CreateEmailCategoryCommand(Guid OrganisationId, string CategoryName):IRequest;
}