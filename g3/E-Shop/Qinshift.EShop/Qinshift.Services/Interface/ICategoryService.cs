using Qinshift.EShop.DTOs.Category;

namespace Qinshift.EShop.Services.Interface
{
	public interface ICategoryService
	{
		IEnumerable<CategoryDto> GetAllCategories();
		CategoryDto GetCategoryById(int id);
		int CreateCategory(CategoryDto categoryDto);
	}
}
