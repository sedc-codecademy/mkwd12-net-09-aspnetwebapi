using Qinshift.MovieRent.DataAccess.DataSource;
using Qinshift.MovieRent.DataAccess.Implementation;
using Qinshift.MovieRent.DataAccess.Interface;
using Qinshift.MovieRent.DomainModels;
using Qinshift.MovieRent.Services.Implementation;
using Qinshift.MovieRent.Services.Interface;

namespace Qinshift.MovieRent.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<IMovieService, MovieService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
