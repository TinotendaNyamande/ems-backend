using AutoMapper;
using EMS.Application.Dtos.Organisation;
using EMS.Application.Features.Organisations.Commands.CreateOrganisation;
using EMS.Domain.Models;

namespace EMS.Application.Common.Mapping
{
    public class OrganisationMappingProfile:Profile
    {
        public OrganisationMappingProfile()
        {
            CreateMap<CreateOrganisationCommand, Organisation>()
                .ConstructUsing(x=> new Organisation(x.OwnerId,x.Name));
            CreateMap<Organisation, OrganisationDto>();
            CreateMap<Organisation, OrganisationDetailsDto>();
               
        }
    }
}
