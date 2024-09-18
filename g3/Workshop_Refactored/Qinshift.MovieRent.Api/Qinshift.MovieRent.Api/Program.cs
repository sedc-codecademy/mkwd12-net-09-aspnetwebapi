using Microsoft.EntityFrameworkCore;
using Qinshift.MovieRent.Services.Implementation;
using Qinshift.MovieRent.Services.Interface;
using Qinshift.MovieRent.Services.Helpers;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;

namespace Qinshift.MovieRent.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Host.UseSerilog((ctx, lc) =>
            {
                lc.WriteTo.File($"logs.txt", 
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}");
                lc.MinimumLevel.Debug();
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Movie rent API", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            // This is how you read all the values from appSettings.json from the AppSettings section

            // On the following line you are getting the 'AppSettings' section from the appSettings.json
            var appConfig = builder.Configuration.GetSection("AppSettings");

            // On the following line you are creating an instance of AppSettings class that will
            // have values for the properties same as the values in the AppSettings section in appSettings.json
            builder.Services.Configure<AppSettings>(appConfig);

            // On the following line you are storing the instance of AppSettings class 
            // in an appSettings variable
            var appSettings = appConfig.Get<AppSettings>();


            //string connString = builder.Configuration.GetConnectionString("ConnectionString");

            builder.Services.RegisterDbContext(appSettings.ConnectionString);
            builder.Services.RegisterRepositories();


            builder.Services.AddTransient<IMovieService, MovieService>();
            builder.Services.AddTransient<IUserService, UserService>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //var secret = builder.Configuration.GetValue<string>("Secret");
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;

                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret)),
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(x =>
            {
                x.AllowAnyOrigin();
            });
            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
