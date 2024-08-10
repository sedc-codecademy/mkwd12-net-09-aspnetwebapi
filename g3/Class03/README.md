# Tools and techniques for working with APIs 🚒

When we work with API applications we work with the client and server-side separately. This means that it would be great if there is some extra help to send precise requests with ease as well as accept data without the hassle of deserialization. This is why there are many features in the ASP.NET Web API applications as well as tools for doing things quicker and easier.

## How to handle parameters 🔸

When we are communicating from the client with a server there are a few ways to send a specific value or data. We can either add it to the body or we can send it through parameters. Parameters are values that we add to the address to that we sent the request. The difference between both ways is that in the body we can add objects using JSON for example or add large amounts of data in a structured way. It also means that everything that we are sending is not visible to the client. The parameters are directly written in the address and they are visible to the client. Usually, the body is used for the data that we are sending to the server to save or edit objects or collections. Parameters are used when the logic of the application requires a value to be visible in the address, or when we are already sending some other type in the body.

### Query Parameters 🔽

Query parameters are part of the query string. A query string is part of the URL address and it is used for holding and structuring parameters that would be sent to the server. The query string has a structure and rules on how to write it. To indicate that we are starting a query string we need to enter **?** after the address. After that, we add **key/value** pairs of parameters that are divided by **&** sign. Special characters such as spaces, tabs, special letters, etc. can be added to a query string by encoding the character. Encoding is replacing characters that can't be used with characters that are allowed in the encoding type. For example, if we need to put space in a value we can either replace it with **+** or **%20**. Many websites check URLs with query parameters.

![query parameters](img/query.png)

#### Example

```text
http://mywebsite.com/api/dogs?type=good+boi&size=large
Result:
 - Request at http://mywebsite.com/api/dogs
 - Query parameters:
  - type: good boi
  - size: large
```

### Path Variables 🔽

Path variables or route parameters are values that we pass directly to the address as part of the address itself. This way of sending parameters is very simple and intuitive to use. The drawback is that we need to set a route in the action that will wait for that precise address, unlike the query parameters that can be added to any address. This makes the path variables more strict and precise. When building restful applications it is a good practice to use path variables whenever possible.

## Model Binding 🔸

### Example

```text
http://mywebsite.com/api/dogs/good
Result:
 - Request at http://mywebsite.com/api/dogs/good
 - Route in controller: [HttpGet("good")] or [Route("good")]
```

### FromBody 🔽

When receiving data we can use the Request object and StreamReader to read the data. If it is a string we can use the body as is, but most of the time we will get an object that we will need to deserialize so we can use it. This is where the features of the ApiController come into play. HTTP requests with data can be automatically deserialized by using the **[FromBody]** attribute as a parameter of our action.

```json
// Data sent in the body of the request
{
 "text": "Buy Milk",
 "color": "blue"
}
```

```csharp asp
public class Note
{
 public string Text {get;set;}
 public int Color {get;set;}
}
...
...
// http://mywebsite.com/api/notes
[HttpPost]
public IActionResult Post([FromBody] Note note)
{
 // user object is populated with the json data
}
```

### FromQuery 🔽

Binding query parameters to parameters in an action can be done in two ways. The first way is to write the key of the query parameter as the name of the parameter in the action. This way the value will be bound to the name. But if we have an object ( not a good practice ) and its properties are query parameters then the automatic matching method will not work. This is where we can write **[FromQuery]** before the parameter and add the C# class as a type. With this attribute, all the query parameters that match the class properties will automatically match.

#### Without FromQuery

```csharp asp
//http://localhost:64329/api/notes?text=Buy+Milk
[HttpPost]
public IActionResult Post(string text)
{
 // text = "Buy Milk"
}
```

#### FromQuery

```csharp asp
//http://localhost:64329/api/notes?text=Buy+Milk&color=green
[HttpPost]
public IActionResult Post([FromQuery] Note note)
{
 // note = { text: "Buy Milk", color: "green"}
}
```

#### Multiple FromQuery

```csharp asp
//http://localhost:64329/api/notes?text=Buy+Milk&color=green&tag=Low+Priority
[HttpPost]
public IActionResult Post([FromQuery] Note note, [FromQuery(Name = "tag")] string tag)
{
 // note = { text: "Buy Milk", color: "green"}
 // tag = "Low Priority"
}
```

### FromHeader 🔽

Headers can be requested through the Request object as in everything the request holds. A quicker and more precise way of getting a header would be to wait for it in a parameter from an action. If we add an **[FromHeader]** attribute before a parameter it would wait for a header with that name and bind it to the parameter. We can also specify a name in the FromHeader attribute.

#### Precise binding

```csharp asp
[HttpPost]
public IActionResult Post([FromHeader] string host)
{
 // host = http://localhost:64329
}
```

#### Name binding

```csharp asp
[HttpPost]
public IActionResult Post([FromHeader(Name = "Accept-Language")] string lang)
{
 // lang = en-US
}
```

## Postman 🔸

Postman is one of the most helpful tools when working with APIs. It makes sending requests and receiving responses very easy. This is why it's one of the most popular tools for testing API applications. Some of the main features of Postman are:

* Supporting a large verity of HTTP methods
* Sending a quick and easy request with headers, body, and even a cookie
* Easy importing/exporting of request collections
* Testing API feature

### Installation 🔽

The postman application can be downloaded from [here](https://www.getpostman.com/downloads/). The installation is very easy. You can use postman out of the box or log in to have some extra perks. There is also a chrome extension that can help you send a request right from the browser.

### Sending Requests 🔽

Sending requests in postman is very easy. We open postman, choose HTTP action from the dropdown menu, and write the URL in the URL bar. Then we can choose from the following options:

* Params - The params tab allows us to write query parameters in input fields. These query parameters automatically attach to the URL that we wrote above.
* Authorization - If we need to authorize the request in some way, in this tab we can choose a variety of methods for achieving authorization with the request
* Headers - In this tab, we can write headers in input fields. These headers will be sent with the request in the headers section.
* Body - Here we can write a body. The body can be written raw, with text, in the form of a form-data, and in some other methods as well. When writing the body we can specify what type it is. For instance, when writing JSON we can pick JSON from a dropdown menu.
* pre-request scripts - We can write scripts that execute before the request is sent
* Tests - Here we can write JavaScript tests that will rely on the request that we are sending

### Collections 🔽

On the left of the request window, there are two important tabs. History, lets you see what requests you were sending in the past and collections. A collection is a place where we keep folders with already written requests. We can create a collection and then save requests into it so that it is easier to back up test requests for an API or send the requests that you are testing the API to a friend or a co-worker.

## Swagger 🔸

Unlike postman which is an application for making requests, swagger is a library. This library is installed on a project that is WebApi. When installed it's configured. When the configuration is done and the application is started, we can access swagger UI. Swagger just by looking at the code can figure out which actions and routes are used, and what they await and even generate dummy data to send and test the API out. This UI can be very helpful when testing or monitoring an API.

### How to setup swagger 🔽

The setup of the swagger is very easy. There are a few configuration lines that need to be added in startup.cs. Documentation on installing and setting up swagger can be found [here](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2&tabs=visual-studio).

#### Swagger config

* install Swashbuckle.AspNetCore as a NuGet package

```csharp asp
// Program.cs
builder.Services.AddSwaggerGen();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

### How to use swagger 🔽

With this configuration, we have an HTML page that has UI for testing our actions and controllers. We can access it by running the application and accessing the /swagger/index.html route.

## Extra Materials 📘

* [URL Encoding Codes](http://www.cheat-sheets.org/sites/html.su/urlencoding.html)
* [Check URL and Query Parameters Online](https://www.freeformatter.com/url-parser-query-string-splitter.html)
* [Testing your API with postman](https://medium.com/aubergine-solutions/api-testing-using-postman-323670c89f6d)
* [ASP.NET Core Model Binding](http://hamidmosalla.com/2017/07/06/asp-net-core-model-binding-controlling-the-binding-source/)
* [Installing swagger](https://dev.to/lucas0707/how-to-quickly-install-swagger-in-a-net-core-application-jkc)
