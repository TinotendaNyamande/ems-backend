
using EMS.Application.Dtos.Auth;

namespace EMS.Application.Dtos.Organisation
{
    public class OrganisationDetailsDto:OrganisationDto
    {
        public UserDto Owner { get; set; }
    }
}
