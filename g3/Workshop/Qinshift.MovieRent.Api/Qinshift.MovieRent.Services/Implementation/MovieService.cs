using Qinshift.MovieRent.DataAccess.DataSource;
using Qinshift.MovieRent.DataAccess.Interface;
using Qinshift.MovieRent.DomainModels;
using Qinshift.MovieRent.DomainModels.Enums;
using Qinshift.MovieRent.DTOs;
using Qinshift.MovieRent.DTOs.Enums;
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
            return _movieRepo.GetAll().Select(x => new MovieDto
            {
                Title = x.Title,
                Plot = x.Plot,
                ReleaseDate = x.ReleaseDate,
                Genre = x.Genre.ToString()
            }).ToList();
        }

        public MovieDto GetMovieById(int id)
        {
            var movie = _movieRepo.GetById(id);
            return new MovieDto
            {
                Title = movie.Title,
                Plot = movie.Plot,
                ReleaseDate = movie.ReleaseDate,
                Genre = movie.Genre.ToString()
            };
        }

        public int AddNewMovie(CreateMovieDto movie)
        {
            Movie movieModel = new();
            if (movie != null)
            {
                movieModel = new()
                {
                    Id = StaticDb.MovieId,
                    Title = movie.Title,
                    Plot = movie.Plot,
                    ReleaseDate = movie.ReleaseDate,
                    Genre = (Genre)movie.Genre
                };
                _movieRepo.Add(movieModel);
            }
            return movieModel.Id;
        }

        public int UpdateMovie(CreateMovieDto movie, int id)
        {
            var existingMovie = _movieRepo.GetById(id);
            if (existingMovie == null)
            {
                return 0;
            }
            existingMovie.Title = movie.Title;
            existingMovie.Plot = movie.Plot;
            existingMovie.ReleaseDate = existingMovie.ReleaseDate;
            existingMovie.Genre = (Genre)movie.Genre;

            return existingMovie.Id;
        }

        public int DeleteMovie(int id)
        {
            var existingMovie = _movieRepo.GetById(id);
            if (existingMovie != null)
                _movieRepo.Remove(id);
            return 1;
        }
    }
}
