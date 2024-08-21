using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Qinshift.EShop.DataAccess;
using Qinshift.EShop.DataAccess.Implementation;
using Qinshift.EShop.DataAccess.Interface;

namespace Qinshift.EShop.Services.Helpers
{
	public static class DIModule
	{
		public static IServiceCollection RegisterDbContext(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<EShopDbContext>
				(opts => opts.UseSqlServer("Server=.;Database=EShopDb;Trusted_Connection=True"));

			return services;
		}

		public static IServiceCollection RegisterRepositories(this IServiceCollection services)
		{
			services.AddTransient(typeof(IRepository<>), typeof(DataRepository<>));
			services.AddTransient<ICategoryRepository, CategoryRepository>();

			return services;
		}
	}
}
