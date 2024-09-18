using AutoMapper;
using Qinshift.MovieRent.DomainModels;
using Qinshift.MovieRent.DTOs;

namespace Qinshift.MovieRent.Services.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.FullName, y => y.MapFrom(z => $"{z.FirstName} {z.LastName}"))
                .ForMember(x => x.UserName, y => y.Ignore())
                .ReverseMap();
        }
    }
}
