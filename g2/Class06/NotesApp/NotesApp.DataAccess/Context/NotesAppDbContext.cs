using Microsoft.EntityFrameworkCore;
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
            //modelBuilder.Entity<Note>().Property(p => p.Text).IsRequired().HasMaxLength(1000);
            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("Note");

                // Define primary key
                entity.HasKey(e => e.Id);

                //
                entity.Property(e => e.Text)
                      .IsRequired() // Not Null
                      .HasMaxLength(1000); // nvarchar(1000)

                //
                entity.Property(e => e.Priority)
                      .HasConversion<int>()
                      .HasDefaultValue(Priority.Low);

                entity.Property(e => e.Tag)
                     .HasConversion<int>();

                // Configure the relationship with User entity
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Notes)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Note_User_UserId");

                //
                //entity.HasQueryFilter(e => e.Text.Length > 0);
            });

            modelBuilder.Entity<User>(entity => 
            {
                entity.ToTable("User");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.LastName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Username)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Password)
                      .IsRequired();

                //
                entity.Ignore(e => e.Age);

                // Create unique index on Username property
                entity.HasIndex(e => e.Username)
                      .IsUnique()
                      .HasDatabaseName("IX_User_Username");
            });

            // Seed Data

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Bob", LastName = "Bobsky", Username = "bobbobsky", Password = "password123" },
                new User { Id = 2, FirstName = "Jane", LastName = "Smith", Username = "janesmith", Password = "password123" }
            );

            // Seed Notes
            modelBuilder.Entity<Note>().HasData(
                new Note { Id = 1, Text = "Complete project report", Priority = Priority.High, Tag = Tag.Work, UserId = 1 },
                new Note { Id = 2, Text = "Go to the gym", Priority = Priority.Medium, Tag = Tag.Health, UserId = 2 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
