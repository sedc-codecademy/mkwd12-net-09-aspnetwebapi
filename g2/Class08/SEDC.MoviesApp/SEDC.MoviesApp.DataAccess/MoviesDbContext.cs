using Microsoft.EntityFrameworkCore;
using SEDC.MoviesApp.Domain;

namespace SEDC.MoviesApp.DataAccess
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions options)
        : base(options) { }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

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
                    Year = 1970
                },
                new Movie()
                {
                    Id = 2,
                    Title = "Unfrozen",
                    Description = "Wellsa was a failed cryogenic scientist, destined to unfreeze people that have been frozen.",
                    Genre = GenreEnum.ScienceFiction,
                    Year = 2020
                }
                );
        }
    }
}
