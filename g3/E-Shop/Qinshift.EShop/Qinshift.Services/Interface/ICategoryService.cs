using Qinshift.EShop.DTOs.Category;

namespace Qinshift.EShop.Services.Interface
{
	public interface ICategoryService
	{
		IEnumerable<CategoryDto> GetAllCategories();

	}
}
