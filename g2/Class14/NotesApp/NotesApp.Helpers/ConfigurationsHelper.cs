using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NotesApp.Helpers
{
    public static class ConfigurationsHelper
    {
        public static void ConfigureAuthentication(this IServiceCollection services, string secretKey)
        {
            // ===> Configure JWT
            services.AddAuthentication(x =>
            {
                //use JWT auth
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    //we expect the token into the HttpContext
                    x.SaveToken = true;
                    //validate token
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey))
                    };
                });
        }

        public static void ConfigureCORSPolicy(this IServiceCollection services)
        {
            // ===> Add CORS service
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowSpecificOrigins",
            //        policy =>
            //        {
            //            policy.WithOrigins("https://example.com") // Replace with your allowed origins
            //                  .AllowAnyHeader()
            //                  .AllowAnyMethod();
            //        });
            //});

            // ===> Add CORS service to allow all origins
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin()   // Allows all origins
                              .AllowAnyHeader()   // Allows all headers
                              .AllowAnyMethod();  // Allows all methods
                    });
            });
        }
    }
}
