using Api_DGA.Application.Dtos.Auth;
using Api_DGA.Core.Entities;
using AutoMapper;

namespace Api_DGA.Application.Mappings
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<User, UserInfoDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => 
                    src.UserRoles.Select(ur => ur.Role.Name).ToList()));
        }
    }
}
