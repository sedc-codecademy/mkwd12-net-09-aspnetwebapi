using Microsoft.EntityFrameworkCore;
using NotesApp.DataAccess.Context;
using NotesApp.Helpers;
using NotesApp.Shared.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===> Configure Serilog
// Log.Logger => the globally shared logger
Log.Logger = ConfigurationsHelper.GetSerilogConfiguration();
// Use Serilog as the logging provider
builder.Host.UseSerilog();
    
// ===> Another approach for managing configurations
// Retrieve the "NotesAppSettings" section from appsettings.json
var notesSettings = builder.Configuration.GetSection("NotesAppSettings");
// Bind the "NotesAppSettings" section to the NotesAppSettings class using IOptions pattern
builder.Services.Configure<NotesAppSettings>(notesSettings);
// Directly use instance of NotesAppSettings in Program.cs
NotesAppSettings notesAppSettings = notesSettings.Get<NotesAppSettings>();

// ===> Register Database
//string connectionString = "Server=.\\SQLEXPRESS;Database=NotesAppDb;Trusted_Connection=True;Integrated Security=True;Encrypt=False;"; BAD WAY
//string connectionString = builder.Configuration.GetConnectionString("NotesAppSqlExpress");
//builder.Services.AddDbContext<NotesAppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<NotesAppDbContext>(options => options.UseSqlServer(notesAppSettings.ConnectionString));

// ===> DEPENDENCY INJECTION
//DependencyInjectionHelper.InjectRepositories(builder.Services);
//DependencyInjectionHelper.InjectServices(builder.Services);
builder.Services.InjectRepositories();
builder.Services.InjectServices();

// ===> Configure JWT
builder.Services.ConfigureAuthentication(notesAppSettings.SecretKey);

// ===> Add CORS service
builder.Services.ConfigureCORSPolicy();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseSerilogRequestLogging(); // Logs every request

//app.UseCors("AllowSpecificOrigins"); // Use the defined CORS policy
app.UseCors("AllowAll"); // Use the "AllowAll" CORS policy

app.UseHttpsRedirection();

app.UseAuthentication(); // to be able to use JWT authentication
app.UseAuthorization();

app.MapControllers();

app.Run();
