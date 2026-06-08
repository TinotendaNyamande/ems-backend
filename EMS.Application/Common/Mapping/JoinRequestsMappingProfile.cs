using AutoMapper;
using EMS.Application.Features.JoinRequest.Commands.CreateJoinRequest;
using EMS.Domain.Models;

namespace EMS.Application.Common.Mapping
{
    public class JoinRequestsMappingProfile:Profile
    {
        public JoinRequestsMappingProfile()
        {
            CreateMap<CreateJoinRequestCommand,JoinRequest>();
        }
    }
}
