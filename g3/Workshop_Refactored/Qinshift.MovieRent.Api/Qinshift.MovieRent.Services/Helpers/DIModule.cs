using Microsoft.Extensions.DependencyInjection;
using Qinshift.MovieRent.DataAccess.Implementation;
using Qinshift.MovieRent.DataAccess.Interface;
using Qinshift.MovieRent.DataAccess;
using Qinshift.MovieRent.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Qinshift.MovieRent.Services.Helpers
{
    public static class DIModule
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MovieRentDbContext>
                (opts => opts.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
