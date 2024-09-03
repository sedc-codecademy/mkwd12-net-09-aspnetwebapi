using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Qinshift.MovieApp.Data;
using Qinshift.MovieApp.Data.Implementations;
using Qinshift.MovieApp.Data.Interfaces;
using Qinshift.MovieApp.Services.Implementations;
using Qinshift.MovieApp.Services.Interfaces;

namespace Qinshift.MovieApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MoviesDbContext>(x =>
                x.UseSqlServer(connectionString));
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IMovieRepository, MovieRepository>();
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IMovieService, MovieService>();
        }
    }
}
