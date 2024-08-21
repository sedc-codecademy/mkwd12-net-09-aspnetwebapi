using Microsoft.EntityFrameworkCore;
using Qinshift.EShop.DomainModels;

namespace Qinshift.EShop.DataAccess
{
	public class EShopDbContext : DbContext
	{
        public EShopDbContext(DbContextOptions options)
            :base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Category>()
                .HasMany(cat => cat.Products)
                .WithOne(prod => prod.Category)
                .HasForeignKey(prod => prod.CategoryId);

            modelBuilder.Entity<Product>()
                .HasMany(prod => prod.Reviews)
                .WithOne(rev => rev.Product)
                .HasForeignKey(rev => rev.ProductId);


            PopulateDb.Seed(modelBuilder);
		}
	}
}
