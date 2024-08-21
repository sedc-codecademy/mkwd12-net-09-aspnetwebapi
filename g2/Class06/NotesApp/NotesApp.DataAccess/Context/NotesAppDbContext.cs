using Microsoft.EntityFrameworkCore;
using NotesApp.DataAccess.Extensions;
using NotesApp.Domain.Enums;
using NotesApp.Domain.Models;

namespace NotesApp.DataAccess.Context
{
    public class NotesAppDbContext : DbContext
    {
        public NotesAppDbContext(DbContextOptions<NotesAppDbContext> options) : base(options) { }

        // DbSets represent tables in the database
        public DbSet<Note> Note { get; set; }
        public DbSet<User> User { get; set; }

        // Configuring Entities using FluentApi
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Note entity
            // modelBuilder.Entity<Note>().Property(p => p.Text).IsRequired().HasMaxLength(1000); // bad way
            modelBuilder.ConfigureNoteEntity();

            // Configure User entity
            modelBuilder.ConfigureUserEntity();

            // Seed Data
            modelBuilder.SeedData();

            base.OnModelCreating(modelBuilder);
        }
    }
}
