
// ===> Installed NuGet packages :
// Microsoft.EntityFrameworkCore
// Microsoft.EntityFrameworkCore.SqlServer
// Microsoft.EntityFrameworkCore.Tools

// ===> Command used for Database Scaffolding :
// Scaffold-DbContext "Server=.\SQLEXPRESS;Database=TodoAppDb;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Domain/Models -Context AppDbContext -ContextDir Domain/Context


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
