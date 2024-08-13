# Building an E-Shop API - Part 1📒

For the purpose of this course we will develop an E-Shop web api. Particulary, we will work on the Categories, Products and Reviews from the E-Shop.


## Requirements:
- Create domain models for Category, Product and Review
- Create StaticDb class that will serve as a database and will have lists of the entities accordingly
- Create Controllers with the appropriate CRUD endpoints for all the entities
- Create DTO models in order to sent to or recive data from the client

**Bonus**
 - Try yourself at home and implement the rest of the controller that we didn't finish on the class. Anyway, consider that this will be done on the 
 next (Part 2) class dedicated to this E-Shop API.

 ## The Code 
 In the following sections, there are the models and the StaticDb list from the class provided. This should serve as a copy/paste resource
 if you didn't manage to follow at the class, to try and implement the API at home.

## Models

### Category

```csharp
public class Category
{
public string Name { get; set; }
public string Description { get; set; }
public IEnumerable<Product> Products { get; set; }
}

public class Product
{
 public string Name { get; set; }
public string Description { get; set; }
public decimal Price { get; set; }
public int StockQuantity { get; set; }
public string ImageUrl { get; set; }
public int CategoryId { get; set; }
public Category Category { get; set; }
public IEnumerable<Review> Reviews { get; set; }
}
```

### Review

```csharp
public class Review
{
public string ReviewerName { get; set; }
public string Comment { get; set; }
public int Rating { get; set; }
public string ImageUrl { get; set; }
public int ProductId { get; set; }
public Product Product { get; set; }
}
```


## StaticDb data
```csharp
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
```
