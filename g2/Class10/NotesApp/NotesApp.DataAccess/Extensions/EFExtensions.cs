using Microsoft.EntityFrameworkCore;
using NotesApp.Domain.Enums;
using NotesApp.Domain.Models;

namespace NotesApp.DataAccess.Extensions
{
    public static class EFExtensions
    {
        public static void ConfigureNoteEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>(entity =>
            {
                // Set table name in the database
                entity.ToTable("Note");

                // Define primary key
                entity.HasKey(e => e.Id);

                // Configure Text property with required and max length constraints
                entity.Property(e => e.Text)
                      .IsRequired() // Not Null
                      .HasMaxLength(250); // nvarchar(250)

                // Configure Priority & Tag enums to be stored as integers in the database
                entity.Property(e => e.Priority)
                      .HasConversion<int>()
                      .HasDefaultValue(Priority.Low); // if no value is provided for Priority, set Low by default

                entity.Property(e => e.Tag)
                     .HasConversion<int>();

                // Configure the relationship with User entity
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Notes)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Note_User_UserId");

                // Global filter for EF to exclude retriveing entities where the Text property is empty
                // Typically used with IsDeleted property when usinf soft delete
                //entity.HasQueryFilter(e => e.Text.Length > 0);
            });
        }

        public static void ConfigureUserEntity(this ModelBuilder modelBuilder)
        {
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

                // Ignore Age property (not mapped to the database)
                entity.Ignore(e => e.Age);

                // Create unique index on Username property
                entity.HasIndex(e => e.Username)
                      .IsUnique()
                      .HasDatabaseName("IX_User_Username");
            });
        }

        public static void SeedData(this ModelBuilder modelBuilder)
        {
            // Seed Users
            //modelBuilder.Entity<User>().HasData(
            //    new User { Id = 1, FirstName = "Bob", LastName = "Bobsky", Username = "bobbobsky", Password = "password123" },
            //    new User { Id = 2, FirstName = "Jane", LastName = "Smith", Username = "janesmith", Password = "password123" }
            //);

            // Seed Notes
            //modelBuilder.Entity<Note>().HasData(
            //    new Note { Id = 1, Text = "Complete project report", Priority = Priority.High, Tag = Tag.Work, UserId = 1 },
            //    new Note { Id = 2, Text = "Go to the gym", Priority = Priority.Medium, Tag = Tag.Health, UserId = 2 }
            //);
        }
    }
}
