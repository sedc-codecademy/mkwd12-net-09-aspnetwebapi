using Microsoft.EntityFrameworkCore;
using Qinshift.EShop.Services.Helpers;
using Qinshift.EShop.Services.Implementation;
using Qinshift.EShop.Services.Interface;

namespace Qinshift.EShop.API
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

            //get conn string from appSettings.json
            string connString = "Server=.;Database=EShopDb;Trusted_Connection=True";

            builder.Services.RegisterDbContext(connString);
            builder.Services.RegisterRepositories(connString);

            builder.Services.AddTransient<ICategoryService, CategoryService>();

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
