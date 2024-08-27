using Qinshift.EShop.DataAccess.Interface;
using Qinshift.EShop.DomainModels;
using Qinshift.EShop.DTOs.Category;
using Qinshift.EShop.Services.Interface;

namespace Qinshift.EShop.Services.Implementation
{
	public class CategoryService : ICategoryService
	{
		#region EFRepo
		//private readonly ICategoryRepository _categoryRepo;

		//public CategoryService(ICategoryRepository categoryRepo)
		//{
		//	_categoryRepo = categoryRepo;
		//}
		#endregion

		#region ADORepo
		private readonly IRepository<Category> _categoryRepo;

		public CategoryService(IRepository<Category> categoryRepo)
		{
			_categoryRepo = categoryRepo;
		}
		#endregion


		public IEnumerable<CategoryDto> GetAllCategories()
		{
			return _categoryRepo.GetAll().Select(x => new CategoryDto
			{
				Name = x.Name,
				Description = x.Description,
			});
		}

		public CategoryDto GetCategoryById(int id)
		{
			var category = _categoryRepo.GetById(id);
			var categoryDto = new CategoryDto
			{
				Name = category.Name,
				Description = category.Description,
			};

			return categoryDto;
		}

		public int CreateCategory(CategoryDto categoryDto)
		{
			Category category = new()
			{
				Name = categoryDto.Name,
				Description = categoryDto.Description
			};
			return _categoryRepo.Add(category);
		}
	}
}
