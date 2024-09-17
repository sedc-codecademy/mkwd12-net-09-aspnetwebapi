using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
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

        public static ILogger GetSerilogConfiguration(string connectionString)
        {
            return new LoggerConfiguration()
                    // Default log level
                    .MinimumLevel.Information()
                    // Only log warnings and errors from Microsoft
                    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                    // Only log warnings and errors from System
                    .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
                    // ===> Logging destinations (Serilog Sinks)
                    // ==> Log in Console
                    .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] : {Message}{NewLine:1}{Exception:1}")
                    // ==> Log in Text file
                    .WriteTo.File(path: "./Logs/noteAppLogs.txt", outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] : {Message}{NewLine:1}{Exception:1}")
                    // ===> Log to database table (BONUS)
                    .WriteTo.MSSqlServer(
                        connectionString: connectionString,
                        sinkOptions: new MSSqlServerSinkOptions
                        {
                            TableName = "Logs",
                            SchemaName = "dbo",
                            AutoCreateSqlTable = true, // Automatically create the table if it doesn't exist
                            // NOTE: Better way is to create migration for the Logs table and set this to 'false'
                        },
                        restrictedToMinimumLevel : LogEventLevel.Information
                    //columnOptions: new ColumnOptions { } // Define custom columns
                    )
                    .CreateLogger();
        }
    }
}
