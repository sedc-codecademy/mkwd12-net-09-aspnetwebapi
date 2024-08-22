using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Qinshift.NotesAppRefactored.Data;
using Qinshift.NotesAppRefactored.Data.Implementations;
using Qinshift.NotesAppRefactored.Domain.Models;
using Qinshift.NotesAppRefactored.Services.Implementations;
using Qinshift.NotesAppRefactored.Services.Interfaces;

namespace Qinshift.NotesAppRefactored.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services)
        {
            services.AddDbContext<NotesAppDbContext>(x =>
            x.UseSqlServer("Server=(localdb)\\Local;Database=NotesAppRef;Trusted_Connection=True;TrustServerCertificate=True"));
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Note>, NoteRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<INoteService, NoteService>();
        }
    }
}
