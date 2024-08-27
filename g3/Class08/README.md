# Connecting to the database with other frameworks 🚠

## ADO.NET 🍞

ADO.NET or active data object .Net is a collection of components, an integrated library in the .net framework that contains methods and features for communicating with data structures such as databases. These features allow developers to manipulate a database in any shape and form. This means that with it we can do almost anything within our code to a database such as:

* Open connection
* Execute queries
* Execute stored procedures
* Execute transactions
* Close connection

Usually, ORMs use this system and do these things easier and in fewer steps. That is why they are preferred over ADO.NET. But since ORMs are automatic most of the time their performance is much much slower. If we need better performance or more control over our database ADO.NET might be a good solution.

### Using ADO.NET 🔸

To use ADO.NET we need the **System.Data.SqlClient** library. Some of the project templates already contain a reference to this library. If not, it can simply be added through the NuGet package manager. To use ADO.NET we must follow some rules:
0. Set the connection string to the database

1. Create a method for some functionality
2. Open an SQL Connection and provide the provided connection string
3. Create an SQL Command ( Class that represents an instruction to SQL )
4. Create an SQL Data Reader if there is a need to read more data
5. Execute the command
6. Get and convert the data needed
7. Close the SQL Connection

### Connection 🔽

When we need something from the database, we don't just take data. We first have to open a connection to access the database. This connection requires the address to the database, a connection string. After we open a connection we can send requests and work with the database as we see fit. After we are done we have to close the connection. If we don't close our connections we will have ghost connections to our database. Opened, not used, and never closed. These connections can break our communication with the database if we take up all available connections. At that point would not be able to open any more new connections. That is why with ADO.NET we always open and close our connection when we are done with our code.

#### Example

```csharp asp
var connectionString = "Server=.;Database=BooksDb2022;Trusted_Connection=True;";
        
// Creating a connection object with the connection string
SqlConnection connection = new SqlConnection(_connectionString);
connection.Open(); // Opening the connection
// Requests to the database
connection.Close(); // Closing the connection

Console.ReadLine();
```

### Sending an SQL query 🔽

To communicate with the database we first need to create an SQL Command. This class is a representation of an instruction that we can send to the SQL database. The command accepts a query ( string ) and if it is required parameters ( for stored procedures, functions, etc. ). After we add the query and parameters we need to execute the command to the database. This can be done in a few different ways:

* Executing and getting the first column of the table as a result
* Executing and getting the whole data from the result
* Executing the command but only getting rows affected as a result
* Executes the command and gets the result in an XML-friendly form

#### Example

```csharp asp
// Count records
SqlConnection connection = new SqlConnection(_connectionString);
connection.Open();

// We create a command object and set it up
SqlCommand cmd = new SqlCommand(); // Instance
cmd.Connection = connection; // Add connection
cmd.CommandText = "select count(*) from Authors"; // Add query
 
// Executing the command, getting the first row and column of the result and converting it to type int
int authorCount = (int)cmd.ExecuteScalar();

Console.WriteLine($"Authors count: {authorCount}");

connection.Close();
```

### Receiving Data 🔽

When getting data from the SQL Command, we can either get direct data ( Scalar, NonQuery ) or we can get data that needs to be converted and mapped to be used ( Reader and XMLReader ). When we get data that needs mapping and converting, for instance from ExecuteReader, we need to first create an instance of the class SqlDataReader. This class contains the data from the database and has functionalities and features that help us extract the correct information from our results. This is usually used when we need to get a whole table and need to target different columns and cells from the table.
> Important: We need to know the precise names of the columns since Visual Studio won't help us with suggestions

#### Example

```csharp asp
// Retreive data
SqlConnection connection = new SqlConnection(_connectionString);
connection.Open();

SqlCommand cmd = new SqlCommand();
cmd.Connection = connection;
cmd.CommandText = "select ID, Name, DateOfBirth from Authors";
// Executing the command, getting the whole table and storing it in an SqlDataReader object
SqlDataReader dr = cmd.ExecuteReader();

// Variables for the data we need ( Could be an object )
int authorId = 0;
string name = null;
DateTime? dob = null;

// Going through all records in the result table stored in the Data Reader
while (dr.Read())
{
 // Three approaches on how to get data from a table
    switch (approach)
    {
     // Getting data from the number of the column
        case 1:
            authorId = dr.GetInt32(0);
            name = dr.GetString(1);
            break;
        // Getting data from the number of the column ( generic )
        case 2:
         authorId = dr.GetFieldValue<int>(0);
            name = dr.GetFieldValue<string>(1);
            break;
        // Getting data from the names of the columns
        case 3:
            authorId = (int)dr["ID"];
            name = (string)dr["Name"];
            break;
    }
// Checking if a column is null ( to not get error while casting it in to DateTime )
    dob = dr.IsDBNull(2) ? (DateTime?)null : (DateTime)dr["DateOfBirth"];

    Console.WriteLine($"ID: {authorId} - Name:{name} - DateOfBirth:{dob}");
}

connection.Close();
```

### Sending parameters to SQL 🔽

When sending parameters to SQL we need to be aware of the dangers that we can expose our database to. There are many ways that we can unintentionally expose our database to our users. The process of a user sending something in an input to harm or infiltrate our SQL database is called **SQL INJECTION**. A good way to avoid this is to make sure that the parameters that we accept are always sent as SQL parameters instead of incorporating them in the query string itself.

#### Bad Example

```csharp asp
// Unsafe filter authors
SqlConnection connection = new SqlConnection(_connectionString);
connection.Open();

Console.Write("Enter author name: ");
string query = Console.ReadLine();

SqlCommand cmd = new SqlCommand();
cmd.Connection = connection;
// Do not send the string that from the user input in to the query directly
cmd.CommandText = "select ID, Name, DateOfBirth from Authors where Name like '%" + query + "%'";

SqlDataReader dr = cmd.ExecuteReader();

int authorId = 0;
string name = null;

while (dr.Read())
{
    switch (approach)
    {
        case 1:
            authorId = dr.GetInt32(0);
            name = dr.GetString(1);
            break;
        case 2:
            authorId = dr.GetFieldValue<int>(0);
            name = dr.GetFieldValue<string>(1);
            break;
    }

    DateTime? dob = dr.IsDBNull(2)
        ? (DateTime?)null
        : (DateTime)dr["DateOfBirth"];

    Console.WriteLine($"AuthorId:{authorId} - Name:{name} - DateOfBirth:{dob}");
}

connection.Close();
```

#### Good Example

```csharp asp
// Safe filter authors with parameter
SqlConnection connection = new SqlConnection(_connectionString);
connection.Open();

Console.Write("Enter author name: ");
string query = Console.ReadLine();

SqlCommand cmd = new SqlCommand();
cmd.Connection = connection;
// We concatinate the SQL variable in to the query
cmd.CommandText = "select ID, Name, DateOfBirth from Authors where Name like '%'+@authorName+'%'";
// We set the paramter for the user input
cmd.Parameters.AddWithValue("@authorName", query);

SqlDataReader dr = cmd.ExecuteReader();

while (dr.Read())
{
    int authorId = (int)dr["ID"];
    string name = (string)dr["Name"];
    DateTime? dob = dr.IsDBNull(2)
        ? (DateTime?)null
        : (DateTime)dr["DateOfBirth"];

    Console.WriteLine($"AuthorId:{authorId} - Name:{name} - DateOfBirth:{dob}");
}
connection.Close();
```

### Executing stored procedure 🔽

When a complex or often used query is needed, it might be a good idea to create a stored procedure for it. Stored procedures are created in the database or from an SQL Database project. After that, we can execute them using the ADO.NET Framework. The only thing that we need to add is the parameters.
> Important: We need to know the precise name of the parameters since Visual Studio won't help us with suggestions

#### Example

```csharp asp
SqlConnection connection = new SqlConnection(_connectionString);
connection.Open();

Console.Write("Enter author name: ");
string query = Console.ReadLine();

SqlCommand cmd = new SqlCommand();
cmd.Connection = connection;
// We set the type of the command to mark it as a request to a stored procedure
cmd.CommandType = System.Data.CommandType.StoredProcedure;
// We enter the name of the stored procedure
cmd.CommandText = "getAuthors";
// We enter the parameters that the stored procedure needs
cmd.Parameters.AddWithValue("@authorName", query);

SqlDataReader dr = cmd.ExecuteReader();

while (dr.Read())
{
    int authorId = (int)dr["ID"];
    string name = (string)dr["Name"];
    DateTime? dob = dr.IsDBNull(2)
        ? (DateTime?)null
        : (DateTime)dr["DateOfBirth"];

    Console.WriteLine($"{authorId} - {name} - {dob}");
}

connection.Close();
```

#### Output parameter example

```csharp, asp
// Adding an output paramter
cmd.Parameters.Add(new SqlParameter()
{
    ParameterName = "@ID",
    SqlDbType = System.Data.SqlDbType.Int,
    Direction = System.Data.ParameterDirection.InputOutput
});

// Executing the command and not expecting any result
cmd.ExecuteNonQuery();

// Getting the output paramter and converting it to int
int authorId = (int)cmd.Parameters["@ID"].Value;
```

### Creating Transactions 🔽

Transactions in SQL are a common solution when we need to write something securely and without faulty data in our database. This is because the control of the execution of the code onto the database is in our hands. If something is wrong the whole transaction can be rolled back and the database will not be affected by the faulty execution. Like all the things mentioned above, transactions are also supported by ADO.NET and we can use them to make sure that we don't add faulty data to our database because of some exception or error. Here is what the flow would look like if we had a transaction in an ADO.NET method for writing something in the database:  

1. Create a connection
2. Open connection
3. Begin transaction on the current connection
4. Create an SQLCommand
5. Add the connection and the transaction to the SQLCommand
6. Add a query to the SQLCommand
7. Execute the command
8. Commit or Rollback the transaction
    * If everything went well -> Commit
    * If there was an error -> Rollback
9. Close connection

#### Example

```csharp asp
// We get the data from the user for the new author
Console.Write("Enter author name: ");
string authorName = Console.ReadLine();
Console.Write("Enter author date of birth: ");
var input = Console.ReadLine();
var dateOfBirth = string.IsNullOrEmpty(input)
? null
: (DateTime?)DateTime.Parse(input);
Console.Write("Enter author date of death: ");
input = Console.ReadLine();
DateTime? dateOfDeath = string.IsNullOrEmpty(input)
? null
: (DateTime?)DateTime.Parse(input);
int authorId = 0;
Console.Write("Enter novel title: ");
string novelTitle = Console.ReadLine();
Console.Write("Enter novel is read: ");
bool isread = bool.Parse(Console.ReadLine());

SqlConnection connection = new SqlConnection(_connectionString);
connection.Open();

// We begin a transaction
var transaction = connection.BeginTransaction();

try
{
// We execute the createAuthor procedure
SqlCommand cmd = new SqlCommand();
cmd.Connection = connection;
cmd.Transaction = transaction;
cmd.CommandType = System.Data.CommandType.StoredProcedure;
cmd.CommandText = "createAuthor";
cmd.Parameters.AddWithValue("@AuthorName", authorName);
cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
cmd.Parameters.AddWithValue("@DateOfDeath", dateOfDeath);
cmd.Parameters.Add(new SqlParameter()
{
 ParameterName = "@ID",
 SqlDbType = System.Data.SqlDbType.Int,
 Direction = System.Data.ParameterDirection.InputOutput
});

cmd.ExecuteNonQuery();

// We save the authorId so we can add it to the new novel
authorId = (int)cmd.Parameters["@ID"].Value;

cmd = new SqlCommand();
cmd.Connection = connection;
cmd.Transaction = transaction;
cmd.CommandType = System.Data.CommandType.StoredProcedure;
cmd.CommandText = "createNovel";
cmd.Parameters.AddWithValue("@Title", novelTitle);
cmd.Parameters.AddWithValue("@AuthorId", authorId);
cmd.Parameters.AddWithValue("@IsRead", isread);
cmd.Parameters.Add(new SqlParameter()
{
 ParameterName = "@ID",
 SqlDbType = System.Data.SqlDbType.Int,
 Direction = System.Data.ParameterDirection.InputOutput
});

cmd.ExecuteNonQuery();

// If all the code above passed with no exception the transaction will be committed ( the changes will reflect on to the database )
transaction.Commit();
}
catch (Exception ex)
{
 // If there was an exception then the transaction will rollback the changes made
transaction.Rollback();
Console.WriteLine(ex.Message);
}
connection.Close();
```

## Dapper 🥨

Dapper is an ORM framework for .NET applications. It bridges the gap between an application and a relational database such as an SQL database. This ORM is built by the guys over at stack overflow and it is famous for being very simple and speedy.

### Benefits of Dapper 🔸

Dapper is built for speed and working with a lot of data. Dapper allows:

* High performance when manipulating data
* Working great with a lot of data
* Mapping DB to models without context
* Easy handling for stored procedures
* Support for sending multiple queries
* Short and simple syntax

Since it is a high-performance ORM it is used for applications where there is a lot of data, a lot of relations, or intensive usage. Dapper can be installed in an application that already has another ORM such as Entity Framework. For instance, if one part of the application is used frequently and is slow it can be migrated to dapper for communicating with the database. One of the main problems with dapper is that it works with ADO.NET very closely and we need to write naked queries. This means that we have to write SQL on the spot or call procedures, unlike entity framework where we just write LINQ and get our data instantly.

### Setup 🔽

Dapper works on top of the ADO.NET framework. Entity Framework does too but dapper uses ADO.NET features directly and more closely. This means that if we want to use Dapper we need to have ADO.NET installed. ASP.NET applications come with ADO.NET already in their dependencies and there is no need to install it. But if there is a need for Dapper installation on any other project type such as Class Library or Console Application the following packages should be installed:

* System.Data.SqlClient
* Dapper

### How does it work 🔸

Dapper does not have the option of Code First approach such as entity framework. We have to have a database first before we can work with dapper. Dapper can map the database with our models automatically. For this mapping we need to create our models exactly like our database, tables are classes and properties are columns just like Entity Framework. After that, we need to open a connection and write a query or call a procedure on that connection. After that, we close the connection. Because it is not a good practice to write naked queries it is better to write stored procedures for doing operations in the database.

#### Classes

```csharp
 public class Author
{
    public int ID { get; set; }
    public string Name { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? DateOfDeath { get; set; }
    public List<Novel> Novels { get; set; }
}
```

```csharp
public class Novel
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
    public List<Nomination> Nominations { get; set; }
}
```

```csharp
public class Nomination
{
    public int ID { get; set; }
    public int BookID { get; set; }
    public int AwardID
    {
        get
        {
            return (int)Award;
        }
        set
        {
            Award = (AwardType)value;
        }
    }
    public int YearNominated { get; set; }
    public bool IsWinner { get; set; }
    public AwardType Award { get; set; }
    public Novel Book { get; set; }
}

public enum AwardType
{
    Hugo = 1,
    Nebula = 2
}
```

#### Connection

```csharp
IDbConnection Connection = new SqlConnection(connectionString);
Connection.Open();
```

#### Simple Query

```csharp
List<Novel> novels = Connection.Query<Novel>("SELECT * FROM Novels").ToList();
Connection.Close();
```

#### Complex Query ( Multiple queries )

```csharp
List<Novel> novels = new List<Novel>();
using (var multi = Connection.QueryMultiple("SELECT * FROM Novels; SELECT * FROM Nominations"))
{
    novels = multi.Read<Novel>().ToList();
    foreach (var novel in novels)
    {
        novel.Nominations.Add(multi.Read<Nomination>()
        .Where(x => x.BookID == novel.ID).Single());
    }
}
Connection.Close();
```

#### Stored Procedure

```csharp
List<Author> authors = Connection.Query<Author>("dbo.getAuthors",
                new { authorName = nameFragment },
                commandType: CommandType.StoredProcedure).ToList();
```

## Extra Materials 📘

* [Introduction to ADO.NET](https://www.c-sharpcorner.com/uploadfile/puranindia/what-is-ado-net/)
* [Microsoft Examples of ADO.NET](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ado-net-code-examples)
* [Dapper Documentation](https://dapper-tutorial.net/dapper)
* [Article on how to use Dapper with C#](https://www.infoworld.com/article/3025784/how-to-work-with-dapper-in-c.html)
* [CRUD methods with Dapper](https://github.com/ericdc1/Dapper.SimpleCRUD/)
* [EF/Dapper/ADO.NET Comparison](https://exceptionnotfound.net/dapper-vs-entity-framework-vs-ado-net-performance-benchmarking/)
* [SQL Injection](https://www.w3schools.com/sql/sql_injection.asp)
