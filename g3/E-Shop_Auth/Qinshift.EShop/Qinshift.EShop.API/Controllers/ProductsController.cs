using Microsoft.AspNetCore.Mvc;

namespace Qinshift.EShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //// GET: api/products
        //[HttpGet]
        //public ActionResult<IEnumerable<ProductDto>> Get()
        //{
        //    var products = StaticDb.Products.Select(x => new ProductDto
        //    {
        //        Name = x.Name,
        //        Description = x.Description,
        //        Price = x.Price,
        //        ImageUrl = x.ImageUrl,
        //    }).ToList();

        //    return Ok(products);
        //}

        //// GET: api/products/{id}
        //[HttpGet("{id:int}")]
        //public ActionResult<ProductDto> GetById(int id)
        //{
        //    if (id <= 0 || id > StaticDb.Products.Count)
        //        return BadRequest("Id must have value bigger than zero");

        //    var productDto = StaticDb.Products
        //                            .Where(x => x.Id == id)
        //                            .Select(x => new ProductDto
        //                            {
        //                                Name = x.Name,
        //                                Description = x.Description,
        //                                Price = x.Price,
        //                                ImageUrl = x.ImageUrl
        //                            }).SingleOrDefault();
        //    if(productDto == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(productDto);
        //}


        //// POST: api/products
        //[HttpPost]
        //public ActionResult<ProductDto> Post([FromBody] ProductCreateDto productCreateDto)
        //{
        //    if (productCreateDto == null)
        //        return BadRequest("You must insert all the values for the product!");

        //    var newId = StaticDb.Products.Max(x => x.Id) + 1;
        //    var category = StaticDb.Categories.SingleOrDefault(x => x.Id == productCreateDto.CategoryId);
        //    if(category == null)
        //    {
        //        return NotFound("Not found such category!");
        //    }

        //    var product = new Product
        //    {
        //        Id = newId,
        //        Name = productCreateDto.Name,
        //        Description = productCreateDto.Description,
        //        Price = productCreateDto.Price,
        //        ImageUrl = productCreateDto.ImageUrl,
        //        StockQuantity = productCreateDto.StockQuantity,
        //        CategoryId = productCreateDto.CategoryId,
        //        Category = category
        //    };

        //    StaticDb.Products.Add(product);

        //    return CreatedAtAction(nameof(GetById), new { id = product.Id }, productCreateDto);
        //}


        //// PUT: api/products/{id}
        //[HttpPut("{id:int}")]
        //public IActionResult Put(int id, [FromBody] ProductCreateDto productCreateDto)
        //{
        //    var existingProduct = StaticDb.Products.SingleOrDefault(x => x.Id == id);
        //    var category = StaticDb.Categories.SingleOrDefault(x => x.Id == productCreateDto.CategoryId);

        //    if (existingProduct == null)
        //        return NotFound("Not found such product for update!");

        //    // Update the existing product
        //    existingProduct.Name = productCreateDto.Name;
        //    existingProduct.Description = productCreateDto.Description;
        //    existingProduct.Price = productCreateDto.Price;
        //    existingProduct.StockQuantity = productCreateDto.StockQuantity;
        //    existingProduct.ImageUrl = productCreateDto.ImageUrl;
        //    existingProduct.CategoryId = productCreateDto.CategoryId;
        //    existingProduct.Category = category;

        //    return Ok("Product updated successfully!");

        //}




        //// DELETE: api/products/{id}
        //[HttpDelete("{id:int}")]
        //public IActionResult Delete(int id)
        //{
        //    var product = StaticDb.Products.SingleOrDefault(x => x.Id == id);
        //    if (product == null)
        //        return NotFound($"Not found product with id: {id} for delete");

        //    StaticDb.Products.Remove(product);
        //    return Ok("Product deleted successfully!");
        //}

    }
}
