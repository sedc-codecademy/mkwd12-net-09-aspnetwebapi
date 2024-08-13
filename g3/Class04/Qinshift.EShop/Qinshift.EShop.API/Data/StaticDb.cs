using Qinshift.EShop.API.Models;

namespace Qinshift.EShop.API.Data
{
    public static class StaticDb
    {

        public static List<Category> Categories = new()
        {
            new Category
            {
                Id = 1,
                Name = "Smartphones and Tablets",
                Description = "All sort of smart phones and tablets",
            },
            new Category
            {
                Id = 2,
                Name = "PC and hardware",
                Description = "Different brands of PCs and all type of hardware components.",
            },
            new Category
            {
                Id = 3,
                Name = "Laptops",
                Description = "Different brands of laptops",
            }
        };


        public static List<Product> Products = new()
        {
            new Product
            {
                Id = 1,
                Name = "Iphone 15 Pro 256GB",
                Description = "256GB | 5.8' | 8GB RAM",
                Price = 1000,
                StockQuantity = 50,
                ImageUrl = "iphone15.jpg",
                CategoryId = 1,
                Category = Categories.Where(x => x.Id == 1).SingleOrDefault(),
            },
            new Product
            {
                Id = 2,
                Name = "Lenovo Y700 Gaming laptop",
                Description = "500GB SSD | 17' | 16GB RAM",
                Price = 2000,
                StockQuantity = 20,
                ImageUrl = "lenovoY700.jpg",
                CategoryId = 3,
                Category = Categories.Where(x => x.Id == 3).SingleOrDefault(),
            },
            new Product
            {
                Id = 3,
                Name = "NVIDIA GeForce 4090",
                Description = "16GB VRAM",
                Price = 2200,
                StockQuantity = 10,
                ImageUrl = "graficka.jpg",
                CategoryId = 2,
                Category = Categories.Where(x => x.Id == 2).SingleOrDefault(),
            },
        };
    }
}
