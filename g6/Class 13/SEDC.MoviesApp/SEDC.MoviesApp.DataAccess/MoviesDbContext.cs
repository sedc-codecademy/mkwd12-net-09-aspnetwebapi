using Microsoft.EntityFrameworkCore;
using SEDC.MoviesApp.Domain;
using SEDC.MoviesApp.Domain.Domain;
using System.Text;
using System.Security.Cryptography;

namespace SEDC.MoviesApp.DataAccess
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions options)
        : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
               .HasOne(x => x.User)
               .WithMany(x => x.MovieList)
               .HasForeignKey(x => x.UserId);

            

            modelBuilder.Entity<Movie>()
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes("123456sedc"));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            modelBuilder.Entity<User>()
                .HasData(
                new User()
                {
                    Id = 1,
                    FirstName = "Boby",
                    LastName = "Bobsky",
                    Username = "bob007",
                    Password = hashedPassword
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Bob1",
                    LastName = "Bobsky1",
                    Username = "bob0071",
                    Password = hashedPassword
                });

            modelBuilder.Entity<Movie>()
                .Property(x => x.Year)
                .IsRequired();

            modelBuilder.Entity<Movie>()
                .Property(x => x.Description)
                .HasMaxLength(250);

            modelBuilder.Entity<Movie>()
                .HasData(
                new Movie()
                {
                    Id = 1,
                    Title = "Bames Jond 2",
                    Description = "Bames returns for one last mission to save the president from impending doom.",
                    Genre = GenreEnum.Action,
                    Year = 1970,
                    UserId = 1
                },
                new Movie()
                {
                    Id = 2,
                    Title = "Unfrozen",
                    Description = "Wellsa was a failed cryogenic scientist, destined to unfreeze people that have been frozen.",
                    Genre = GenreEnum.ScienceFiction,
                    Year = 2020,
                    UserId = 1
                }
                );
        }
    }
}
