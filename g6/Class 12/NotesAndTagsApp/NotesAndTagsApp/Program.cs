using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NotesAndTagsApp.Helpers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
                c =>
                {
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                          Enter 'Bearer' [space] and then your token in the text input below.
                          \r\n\r\nExample: 'Bearer 12345abcdef'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                      {
                        {
                          new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                        });
                });

//inject database
//DependencyInjectionHelper.InjectDbContext(builder.Services);

//inject services
DependencyInjectionHelper.InjectServices(builder.Services);

//inject repos
DependencyInjectionHelper.InjectRepositories(builder.Services);


//read from appSettings.json, find the property DbSettings
var appSettings = builder.Configuration.GetSection("DbSettings");
builder.Services.Configure<DatabaseSettings>(appSettings); //map the appSettings into the class DatabaseSettings
DatabaseSettings dbSettings = appSettings.Get<DatabaseSettings>(); //creates an object with values from app settings
var connectionString = dbSettings.ConnectionString;

DependencyInjectionHelper.InjectDbContext(builder.Services, connectionString);

//DependencyInjectionHelper.InjectAdoRepositories(builder.Services, connectionString);
//DependencyInjectionHelper.InjectDapperRepositories(builder.Services, connectionString);


//Configure JWT
builder.Services.AddAuthentication(x =>
{
    //we will use JWT authentication
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    //token configuration

    x.RequireHttpsMetadata = false;
    //we expect the token into the HttpContext
    x.SaveToken = true;
    //how to validate token
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        //the secret key
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Our very secret secretttt secret key"))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); //we always need to do authentication first, before authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
