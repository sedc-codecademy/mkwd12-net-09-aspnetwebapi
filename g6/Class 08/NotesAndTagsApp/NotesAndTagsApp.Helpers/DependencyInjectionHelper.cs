using Microsoft.EntityFrameworkCore;
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
        public static void InjectDbContext(IServiceCollection services)
        {
            services.AddDbContext<NotesAndTagsAppDbContext>(x =>
            x.UseSqlServer("Server=.\\SQLExpress;Database=SEDCNotesAndTagsApp;Trusted_Connection=True;TrustServerCertificate=True"));
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<INoteService, NoteService>();
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Note>, NoteRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();
        }
    }
}
