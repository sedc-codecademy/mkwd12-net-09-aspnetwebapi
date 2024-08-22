# Building a WebAPI Solution with Entity Framework 🥐

Entity Framework is an ORM framework that bridges the gap between the database and our code. Entity framework has the power to create a code structure or database structure automatically depending on the approach we will take to start our project.

## Entity Framework and Console Applications 🔸

Unlike ASP.NETCore MVC applications and other applications don't come with an entity framework preinstalled. This means that we have to install all libraries that are needed for EntityFrameworkCore to work properly. To install EntityFrameworkCore on a console application, open NuGet packages and install these libraries:

* Microsoft.EntityFrameworkCore.SqlServer -> This package is needed to make a connection to an SQL Server. This enables the registration of the context in the DI module
* Microsoft.EntityFrameworkCore.Design -> This package makes Migrations available to use
* Microsoft.EntityFrameworkCore.Tools -> This package is used to have console commands such as "add-migration"

## Scaffolding a context 🔸

One of the approaches that we can choose when starting an ASP.NET application using the Entity framework is the Code first approach. This approach lets us create a context and models that are reflecting the structure of our data and then the entity framework creates a database with all the tables matching our classes and columns matching our properties. This approach can be done in a different direction. But if we already have a database but want our application to work with code-first changes, we can scaffold the context and domain models from the given database. We do this with the Scaffold-DbContext command. The Scaffold-DbContext needs:

* Connection string
* Provider
* Directory ( to store the newly created context and domain models )

### Example

```csharp aspnet
Scaffold-DbContext "Server=.\SQLExpress;Database=BooksDB2022;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Domain
```

## Configuring models 🔸

The configuration of our domain models to match the right structure for our data can be tricky. To configure all the models correctly, there should be a structure for how every domain model communicates and is connected. There is also the question about validations, naming, and constraints. Luckily Entity framework has features that help us configure the database as we see fit from our code without writing queries in our database. This can be done in two ways:

* Data Annotations
* Fluent API

### Data Annotations 🔽

Configuring models with data annotations is easy. This is because we write the annotation above the property or entity we want to configure. We can even add multiple data annotations above one property or entity. Data annotations are easy because they are simple and don't have advanced features such as the Fluent API configurations. Also if we don't want to concern our domain models with database relations it is not a good idea to write Data Annotations.

#### Table

```csharp
[Table("Books")]
public partial class Novels
{
// code...
}
```

#### Column

```csharp
[Column("ID")]
public int Id { get; set; }
```

#### Key

```csharp
[Key]
public int Id { get; set; } 
```

#### ForeignKey

```csharp
[ForeignKey("Author")]
public int AuthorId { get; set; }
```

#### MaxLength and Required ( multiple )

```csharp
[Required]
[MaxLength(150)]
public string Title { get; set; }
```

#### NotMapped

```csharp
[NotMapped]
public int BooksCount { get; set; }
```

### Fluent API 🔽

Fluent API according to Microsoft is a more advanced way of configuring models and context to match a database. Fluent API also has more features and is done directly in the context, meaning that the models are clean from relation and validation configurations

#### HasColumnName

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
 // code...
    modelBuilder.Entity<Authors>(entity =>
    {
     // code...
        entity.Property(e => e.Id)
           .HasColumnName("ID");
    });
    // code...
}
```

#### IsRequired

```csharp
entity.Property(e => e.Name)
   .IsRequired();
```

#### HasMaxLength

```csharp
entity.Property(e => e.Name)
   .HasMaxLength(100);
```

#### HasOne, WithMany and HasForeignKey ( multiple )

```csharp
entity.HasOne(d => d.Award)
      .WithMany(p => p.Nominations)
      .HasForeignKey(d => d.AwardId)
```

#### HasColumnType

```csharp
entity.Property(e => e.DateOfBirth).HasColumnType("date");
```

#### Ignore

```csharp
entity.Ignore(e => e.NominationsCount);
```

## Creating complex queries 🔸

When requesting something from the context we write a query with the needed parameters. Queries can be simple or complex depending on how many layers of data we need. We can get data from a table with a simple query but if we want to get data from other tables connected to that, we need to write more complex queries. Entity framework has methods that we can use to query multiple tables connected in one request.

### Simple Query

```csharp
IQueryable<Authors> result = _context.Set<Authors>()
```

### Complex query ( 2 layers )

```csharp
IQueryable<Authors> result = _context.Set<Authors>()
          .Include(x => x.Novels)
```

### Complex Query ( 4 layers )

```csharp
IQueryable<Authors> result = _context.Set<Authors>()
          .Include(x => x.Novels)
              .ThenInclude(x => x.Nominations)
                  .ThenInclude(x => x.Award);
```

## Extra Materials 📘

* [Entity Framework Core on Console Application](https://www.tektutorialshub.com/entity-framework-core/ef-core-console-application/)
* [Examples of all Data Anotations](https://www.learnentityframeworkcore.com/configuration/data-annotation-attributes)
* [Fluent API Configurations](https://www.learnentityframeworkcore.com/configuration/fluent-api)
