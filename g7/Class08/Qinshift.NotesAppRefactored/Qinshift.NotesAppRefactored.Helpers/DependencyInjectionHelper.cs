using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Qinshift.NotesAppRefactored.Data;
using Qinshift.NotesAppRefactored.Data.AdoImplementations;
using Qinshift.NotesAppRefactored.Data.DapperImplementations;
using Qinshift.NotesAppRefactored.Data.Implementations;
using Qinshift.NotesAppRefactored.Domain.Models;
using Qinshift.NotesAppRefactored.Services.Implementations;
using Qinshift.NotesAppRefactored.Services.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace Qinshift.NotesAppRefactored.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<NotesAppDbContext>(x =>
            x.UseSqlServer(connectionString));
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            //services.AddTransient<IRepository<Note>, NoteRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();
        }

        public static void InjectAdoRepositories(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IRepository<Note>>(yt => new NoteAdoRepository(connectionString));
        }

        public static void InjectDapperRepositories(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IRepository<Note>>(yt => new NoteDapperRepository(connectionString));
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<INoteService, NoteService>();
        }
    }
}
