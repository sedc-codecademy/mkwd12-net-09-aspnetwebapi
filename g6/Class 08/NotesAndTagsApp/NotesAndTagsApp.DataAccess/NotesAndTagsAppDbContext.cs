using Microsoft.EntityFrameworkCore;
using NotesAndTAgsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.DataAccess
{
    public class NotesAndTagsAppDbContext : DbContext
    {
        public NotesAndTagsAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Note> Notes {  get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //with this we take the implementation from the base and we can add our extra configurations to the metod

            modelBuilder.Entity<Note>() //we take the entity
                .Property(x => x.Text) //we take the property from the entity
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Note>()
                .Property(x => x.Priority)
                .IsRequired();

            modelBuilder.Entity<Note>()
                .Property(x => x.Tag)
                .IsRequired();

            //relation
            modelBuilder.Entity<Note>() //the entity Note
                .HasOne(x => x.User) //has one user
                .WithMany(x => x.Notes) //the user has many notes
                .HasForeignKey(x => x.UserId); //the foreign key that connects note to user is the column UserId from Note

            modelBuilder.Entity<User>()
                .Property(x => x.Firstname)
                .HasMaxLength(50);
            
            modelBuilder.Entity<User>()
                .Property(x => x.Lastname)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<User>()
                .Ignore(x => x.Age); //not mapped
        }
    }
}
