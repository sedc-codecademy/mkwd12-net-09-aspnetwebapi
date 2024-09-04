using Microsoft.EntityFrameworkCore;
using Qinshift.MovieRent.DataAccess.DataSource;
using Qinshift.MovieRent.DomainModels;
using Qinshift.MovieRent.DomainModels.Enums;

namespace Qinshift.MovieRent.DataAccess
{
    public class MovieRentDbContext : DbContext
    {
        public MovieRentDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Movie>()
            //    .ToTable("Movies")
            //    .Property(x => x.Title)
            //    .IsRequired();

            InitializeDb.InitDb(modelBuilder);
        }
    }
}
