using Qinshift.MovieRent.DataAccess.DataSource;
using Qinshift.MovieRent.DataAccess.Interface;
using Qinshift.MovieRent.DomainModels;
using Qinshift.MovieRent.DomainModels.Enums;
using Qinshift.MovieRent.DTOs;
using Qinshift.MovieRent.DTOs.Enums;
using Qinshift.MovieRent.Services.Helpers;
using Qinshift.MovieRent.Services.Interface;

namespace Qinshift.MovieRent.Services.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepo;
        public MovieService(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        public List<MovieDto> GetAllMovies()
        {
            var movies = _movieRepo.GetAll();
            var movieListDto = new List<MovieDto>();
            foreach (var movie in movies)
            {
                var movieDto = MovieMapper.ToMovieDto(movie);
                movieListDto.Add(movieDto);
            }
            return movieListDto;
        }

        public MovieDto GetMovieById(int id)
        {
            var movie = _movieRepo.GetById(id);
            return MovieMapper.ToMovieDto(movie);
        }

        public List<MovieDto> FilterMovies(string genre, int year)
        {
            var movies = _movieRepo.GetAll();
            var movieList = new List<MovieDto>();
            if (!string.IsNullOrEmpty(genre))
                movies = movies.Where(x => x.Genre.ToString().ToLower() == genre.ToLower());
            if (year > 0)
                movies = movies.Where(x => x.ReleaseDate.Year == year);

            foreach(var movie in movies)
            {
                var movieDto = MovieMapper.ToMovieDto(movie);
                movieList.Add(movieDto);
            }

            return movieList;
        }

        public int AddNewMovie(CreateMovieDto movie)
        {
            Movie movieModel = new();
            if (movie != null)
            {
                movieModel = MovieMapper.ToMovie(movie);
                return _movieRepo.Add(movieModel);
            }
            return 0;
        }

        public int UpdateMovie(CreateMovieDto movie, int id)
        {
            var existingMovie = _movieRepo.GetById(id);
            if (existingMovie == null)
            {
                return 0;
            }
            return _movieRepo.Update(existingMovie);
        }

        public int DeleteMovie(int id)
        {
            var existingMovie = _movieRepo.GetById(id);
            if (existingMovie != null)
                return _movieRepo.Remove(existingMovie.Id);
            return 0;
        }
    }
}
