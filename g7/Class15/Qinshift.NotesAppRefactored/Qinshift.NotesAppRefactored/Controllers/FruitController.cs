using Microsoft.AspNetCore.Mvc;
using Qinshift.NotesAppRefactored.Services.Interfaces;
using Qinshift.NotesAppRefactored.Shared.CustomExceptions.FruitExceptions;

namespace Qinshift.NotesAppRefactored.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitController : ControllerBase
    {
        private readonly IFruitService _fruitService;

        public FruitController(IFruitService fruitService)
        {
            _fruitService = fruitService;
        }

        [HttpGet("{fruitName}")]
        public async Task<IActionResult> GetFruit(string fruitName)
        {
            try
            {
                var fruitInfo = await _fruitService.GetFruitInfoAsync(fruitName);
                return Ok(fruitInfo);
            }
            catch (FruitNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFruits()
        {
            try
            {
                var fruits = await _fruitService.GetAllFruitsAsync();
                return Ok(fruits);
            }
            catch (FruitDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("family/{familyName}")]
        public async Task<IActionResult> GetFruitsByFamily(string familyName)
        {
            try
            {
                var fruit = await _fruitService.GetFruitByFamilyAsync(familyName);
                return Ok(fruit);
            }
            catch (FruitDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FruitNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("genus/{genusName}")]
        public async Task<IActionResult> GetFruitsByGenus(string genusName)
        {
            try
            {
                var fruits = await _fruitService.GetFruitsByGenusAsync(genusName);
                return Ok(fruits);
            }
            catch (FruitDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FruitNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("order/{orderName}")]
        public async Task<IActionResult> GetFruitsByOrder(string orderName)
        {
            try
            {
                var fruits = await _fruitService.GetFruitsByOrderAsync(orderName);
                return Ok(fruits);
            }
            catch (FruitDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FruitNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
