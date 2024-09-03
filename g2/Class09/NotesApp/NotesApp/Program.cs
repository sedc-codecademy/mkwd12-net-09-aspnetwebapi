using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotesApp.DataAccess.Context;
using NotesApp.Helpers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Database
//string connectionString = "Server=.\\SQLEXPRESS;Database=NotesAppDb;Trusted_Connection=True;Integrated Security=True;Encrypt=False;"; BAD WAY
string connectionString = builder.Configuration.GetConnectionString("NotesAppSqlExpress");
builder.Services.AddDbContext<NotesAppDbContext>(options => options.UseSqlServer(connectionString));

//DEPENDENCY INJECTION
DependencyInjectionHelper.InjectRepositories(builder.Services);
DependencyInjectionHelper.InjectServices(builder.Services);

//Configure JWT
builder.Services.AddAuthentication(x =>
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
            ValidateActor = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Our very secret key for noteApp"))
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

app.MapControllers();

app.Run();
