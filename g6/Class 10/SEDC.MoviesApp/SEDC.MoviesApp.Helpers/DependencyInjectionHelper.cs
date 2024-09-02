using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.MoviesApp.DataAccess;
using SEDC.MoviesApp.Services.Implementations;
using SEDC.MoviesApp.Services.Interfaces;

namespace SEDC.MoviesApp.Helpers
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
