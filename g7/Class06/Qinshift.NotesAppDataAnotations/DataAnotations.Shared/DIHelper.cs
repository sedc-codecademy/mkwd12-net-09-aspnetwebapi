using DataAnotations.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAnotations.Shared
{
    public static class DIHelper
    {
        public static void InjectDbContext(IServiceCollection services)
        {
            services.AddDbContext<NotesAppDbContext>(x =>
            x.UseSqlServer("Server=(localdb)\\Local;Database=NotesAppDataAnnotations;Trusted_Connection=True;TrustServerCertificate=True"));
        }
    }
}
