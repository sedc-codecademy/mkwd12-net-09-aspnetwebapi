using Microsoft.AspNetCore.Authentication.JwtBearer;
using Qinshift.NotesAppRefactored.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//configuring swagger so it can allow us to be logged in and use methods that require authorization
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the bearer scheme, e.g" + 
        "\"bearer {token} \"",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

//read from appSettings.json, we find the property AppSettings from the main object
var appSettings = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettings);

AppSettings appSettingsObject = appSettings.Get<AppSettings>();

//DEPENDENCY INJETCTION =>
DependencyInjectionHelper.InjectDbContext(builder.Services, appSettingsObject.ConnectionString);
DependencyInjectionHelper.InjectRepositories(builder.Services);
DependencyInjectionHelper.InjectServices(builder.Services);

//DependencyInjectionHelper.InjectAdoRepositories(builder.Services, appSettingsObject.ConnectionString);
//DependencyInjectionHelper.InjectDapperRepositories(builder.Services, appSettingsObject.ConnectionString);

builder.Services.AddAuthentication(x =>
{
    //we will use JWT authentication
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(y =>
    {
        y.RequireHttpsMetadata = false;
        //we expect the token to be put in the httpContext
        y.SaveToken = true;
        //how to validate token
        y.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            //the secret key
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Our very secret key"))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();
