using AutoMapper;
using EMS.Application.Dtos.Organisation;
using EMS.Application.Features.Organisations.Commands.CreateOrganisation;
using EMS.Domain.Models;

namespace EMS.Application.Common.Mapping
{
    public class OrganisationMappingProfile : Profile
    {
        public OrganisationMappingProfile()
        {
            CreateMap<CreateOrganisationCommand, Organisation>(MemberList.None)
                .ConstructUsing(x => new Organisation(x.Name, x.OwnerId));
            CreateMap<Organisation, OrganisationDto>();
            CreateMap<Organisation, OrganisationDetailsDto>()
                .ForMember(x => x.Owner, opt => opt.Ignore());
        }
    }
}
