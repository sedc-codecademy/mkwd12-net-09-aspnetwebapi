using AutoMapper;
using Qinshift.MovieRent.DomainModels;
using Qinshift.MovieRent.DTOs;

namespace Qinshift.MovieRent.Services.Mappers
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            CreateMap<Movie, MovieDto>()
                .ReverseMap();
        }
    }
}
