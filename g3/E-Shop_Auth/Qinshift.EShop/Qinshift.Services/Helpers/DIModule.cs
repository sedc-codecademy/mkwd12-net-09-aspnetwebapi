using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Qinshift.EShop.DataAccess;
using Qinshift.EShop.DataAccess.AdonetImplementation;
using Qinshift.EShop.DataAccess.DapperImplementation;
using Qinshift.EShop.DataAccess.Implementation;
using Qinshift.EShop.DataAccess.Interface;
using Qinshift.EShop.DomainModels;

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

		public static IServiceCollection RegisterRepositories(this IServiceCollection services, string connectionString)
		{
			services.AddTransient(typeof(IRepository<>), typeof(DataRepository<>));
			//services.AddTransient<ICategoryRepository, CategoryRepository>();

			services.AddTransient<IRepository<Category>>(x => new CategoryAdoRepository(connectionString));
			//services.AddTransient<IRepository<Category>>(x => new CategoryDapperRepository(connectionString));
			services.AddTransient<IUserRepository, UserRepository>();
			return services;
		}
	}
}
