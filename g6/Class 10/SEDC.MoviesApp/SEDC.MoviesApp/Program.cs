using SEDC.MoviesApp.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// read from appSettings.json, find the property DbSettings
var appSettings = builder.Configuration.GetSection("DbSettings");
builder.Services.Configure<DatabaseSettings>(appSettings); //map the appSettings into the class DatabaseSettings
DatabaseSettings dbSettings = appSettings.Get<DatabaseSettings>(); //creates an object with values from app settings
var connectionString = dbSettings.ConnectionString;

DependencyInjectionHelper.InjectDbContext(builder.Services, connectionString);

DependencyInjectionHelper.InjectRepositories(builder.Services);
DependencyInjectionHelper.InjectServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
