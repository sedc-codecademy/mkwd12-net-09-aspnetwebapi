using Qinshift.EShop.DataAccess.Interface;
using Qinshift.EShop.DomainModels;

namespace Qinshift.EShop.DataAccess.Implementation
{
	public class CategoryRepository : DataRepository<Category>, ICategoryRepository
	{
		public CategoryRepository(EShopDbContext context) : base(context)
		{
		}
	}
}
