using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.MoviesApp.DataAccess;
using SEDC.MoviesApp.Services.Implementations;
using SEDC.MoviesApp.Services.Interfaces;

namespace SEDC.MoviesApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services)
        {
            services.AddDbContext<MoviesDbContext>(x =>
                x.UseSqlServer("Server=.;Database=MoviesDbTest;Trusted_Connection=True"));
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
