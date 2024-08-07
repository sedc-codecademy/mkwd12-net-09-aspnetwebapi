using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Qinshift.NotesApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.Configure<KestrelServerOptions>(opt =>
            {
                opt.AllowSynchronousIO = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            app.UseStaticFiles();


            app.Run();
        }
    }
}
