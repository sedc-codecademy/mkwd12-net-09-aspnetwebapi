using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.DependencyInjection;
using NotesAndTagsApp.DataAccess;
using NotesAndTagsApp.DataAccess.Implementation;
using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTagsApp.Services.Implementation;
using NotesAndTagsApp.Services.Interfaces;
using NotesAndTAgsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<NotesAndTagsAppDbContext>(x =>
           // x.UseSqlServer("Server=.\\SQLExpress;Database=SEDCNotesAndTagsApp;Trusted_Connection=True;TrustServerCertificate=True"));
            x.UseSqlServer(connectionString));
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IUserService, UserService>();
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Note>, NoteRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        //public static void InjectAdoRepositories(IServiceCollection services, string connectionString) {
        //     services.AddTransient<IRepository<Note>>(x => new NoteAdoRepository(connectionString));
        //}

        public static void InjectDapperRepositories(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IRepository<Note>>(x => new NoteDapperRepository(connectionString));
        }
    }
}
