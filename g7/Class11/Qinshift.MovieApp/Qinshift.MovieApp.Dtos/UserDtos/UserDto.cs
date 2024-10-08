﻿using Qinshift.MovieApp.Dtos.MovieDtos;

namespace Qinshift.MovieApp.Dtos.UserDtos
{
    public class UserDto
    {
        public UserDto()
        {
            MovieList = new List<MovieDto>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Token { get; set; }
        public List<MovieDto> MovieList { get; set; }
    }
}
