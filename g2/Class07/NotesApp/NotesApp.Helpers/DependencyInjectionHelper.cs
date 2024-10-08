﻿using Microsoft.Extensions.DependencyInjection;
using NotesApp.DataAccess;
using NotesApp.DataAccess.Implementations;
using NotesApp.Domain.Models;
using NotesApp.Services.Implementation;
using NotesApp.Services.Interface;

namespace NotesApp.Helpers
{
    public static class DependencyInjectionHelper
    {
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
