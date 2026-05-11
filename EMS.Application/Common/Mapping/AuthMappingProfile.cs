using AutoMapper;
using EMS.Application.Dtos.Auth;
using EMS.Application.Features.Auth.Commands.ChangeUserPassword;

namespace EMS.Application.Common.Mapping
{
    public class AuthMappingProfile:Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<ChangeUserPasswordCommand, ChangeUserPasswordDto>();
        }
    }
}
