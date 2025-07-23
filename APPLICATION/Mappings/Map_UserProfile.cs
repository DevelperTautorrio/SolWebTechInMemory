using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APPLICATION.DTOs.Request;
using APPLICATION.DTOs.Response;
using AutoMapper;
using DOMAIN.ENTITIES;

namespace APPLICATION.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            
            CreateMap<UserCreateDto, E_User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<UserUpdateDto, E_User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            
            CreateMap<E_User, UserResponseDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
                    $"{src.FirstName} {src.PaternalSurname} {src.MaternalSurname}".Trim()));

            CreateMap<E_User, UserDetailDto>()
                .IncludeBase<E_User, UserResponseDto>();
        }
    }
}
