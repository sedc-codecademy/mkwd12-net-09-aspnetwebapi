using Microsoft.EntityFrameworkCore;
using Qinshift.MovieRent.Services.Implementation;
using Qinshift.MovieRent.Services.Interface;
using Qinshift.MovieRent.Services.Helpers;

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



            string connString = builder.Configuration.GetConnectionString("ConnectionString");

            builder.Services.RegisterDbContext(connString);
            builder.Services.RegisterRepositories();


            builder.Services.AddTransient<IMovieService, MovieService>();

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
