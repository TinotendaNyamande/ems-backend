using MediatR;

namespace EMS.Application.Features.UsersManagement.Commands.CreateUserForOrganisation
{
    public record CreateUserForOrganisationCommand(string FirstName, string LastName, string Email, string Password,Guid OrganisationId,string Role):IRequest
    {
    }
}
