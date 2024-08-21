using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.DataAcccss;

namespace NotesApp.Helpers
{
    public static class DIHelper
    {
        public static void InjectDbContext(IServiceCollection services)
        {
            services.AddDbContext<NotesAppFluentAPIDbContext>(x =>
            x.UseSqlServer("Server=(localdb)\\Local;Database=NotesAppFluentApiDb;Trusted_Connection=True;TrustServerCertificate=True"));
        }
    }
}
