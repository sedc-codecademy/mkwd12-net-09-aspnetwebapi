using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qinshift.EShop.DTOs.Category;
using Qinshift.EShop.Services.Interface;

namespace Qinshift.EShop.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
		[Authorize]
		public IActionResult Get()
		{
			return Ok(_categoryService.GetAllCategories());
		}

		[HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
		{
			return Ok(_categoryService.GetCategoryById(id));
		}

		[HttpPost]
		[Authorize]
        public IActionResult Post([FromBody]CategoryDto categoryDto)
		{
			_categoryService.CreateCategory(categoryDto);
			return Ok();
		}

	}
}
