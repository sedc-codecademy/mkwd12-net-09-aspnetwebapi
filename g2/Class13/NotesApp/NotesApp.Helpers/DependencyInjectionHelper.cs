using Microsoft.Extensions.DependencyInjection;
using NotesApp.DataAccess;
using NotesApp.DataAccess.Implementations.AdoNetImplementation;
using NotesApp.DataAccess.Implementations.DapperImplementation;
using NotesApp.DataAccess.Implementations.EFImplementation;
using NotesApp.DataAccess.Interfaces;
using NotesApp.Domain.Models;
using NotesApp.Services.Implementation;
using NotesApp.Services.Interfaces;

namespace NotesApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectRepositories(IServiceCollection services)
        {
            // USE DAPPER repository
            //services.AddTransient<IRepository<Note>, NoteDapperRepository>();
            // USE ADO.NET repository
            //services.AddTransient<IRepository<Note>, NoteAdoNetRepository>();
            // USE EF repository
            services.AddTransient<IRepository<Note>, NoteRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
