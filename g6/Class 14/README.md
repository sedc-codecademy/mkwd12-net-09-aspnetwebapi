# Logging and Connecting API to other services 🚁
## Logging 🔶
Logging is an integral part in any application. It is the process where we write code that will document a record with some important data when something is executed in the application. Usually logging is added in an important methods that are worth noting that were executed as well as errors so that when something breaks, a developer can open the text file and read what happened. Visual Studio is a great tool for monitoring our code and tracking bugs and errors, but when we deploy our application on some server such as a test server or a production server we do not have the luxury to open Visual Studio and debug. This is where log files come handy and help us detect problems on the spot. 
### Log messages 🔹
When logging we create and save messages to a file. logging is usually done in a text file but it can be done in any format the developer sees fit. Sometimes logging is done even in a database. But log messages are very different one from another depending on what happened in the code. That is why there are different types of log messages that represent the nature of the message. When using our own logger these messages can be categorized with our own system, but when working with libraries the messages are usually categorized by a convention called Severity Level Directive by their severity:
* Error - An error or exception happened
* Warning - Nothing is broken but there are some suspicious activities
* Info - Information that something happened
* Debug - Some extra data for easier debugging
### Logging Libraries 🔹
Logging can be done without libraries by creating methods that write in a text file. But if there is a need for extra features and automation libraries are definitely a better option. There are a lot of different logging libraries and all of them have some features that make them unique and easy to log information on our application. Most of them can give automatic information about the inner works of the application, add timestamps automatically, read and write automatically as well as give preset Directives for severity. Some of the more famous ones for .NET applications are:
* Log4net
* NLog
* SeriLog
### SeriLog 🔹
All custom loggers need to be configured before they can be used. These configuration are usually trivial and very easy for a basic setup. After they are configured they can be used with out of the box ready methods that log and document things in our application. SeriLog documents a lot of stuff on its own without even writing any log such as starting application, methods called, timestamps, endpoints hit, responses, SQL queries executed and so on. The other logs we can write very easily with the **Log** class. The logging is done by stating the severity status first. This way every log is categorized. This library also has the option to log whole objects so in the logs we can see an object instead of just what type it is.
#### Install and Configure SeriLog ( program.cs )
**Nuget packages:**
* Serilog
* Serilog.AspNetCore ( v 2.1.1 )
* Serilog.Sinks.RollingFile
```csharp asp
public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .WriteTo.RollingFile(
                $@"{AppDomain.CurrentDomain.BaseDirectory}Logs\Notes_LOG_{DateTime.UtcNow.Date:dd-MM-yyyy}.txt",
                LogEventLevel.Information,
                "{NewLine}{Timestamp:HH:mm:ss} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception}")
            .CreateLogger();
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseSerilog()
            .UseStartup<Startup>();
}
```
#### Using SeriLog
```csharp asp
// This will log an Information type log
Log.Information("USER {username} has registered on the map", Username);
// This will log an Error type log
Log.Error("USER Error for {userId}.{name}: {message}", UserId, Name, Message);
// This will log an Error type log but the user object will be presented with all data 
Log.Error("USER Error for {@user}: {message}", User, Message);
```
## Connecting other applications to our API 🔶
### Microservice structure 🔹
Microservice structure and building of projects is a revolutionary way of building applications. It is a very simple concept, instead of building the whole logic inside of one application we can create multiple small applications that will do a certain thing really good independently. After the applications are built they will communicate between each other to create one big implementation and work as one application. This might sound strange, but with this implementation we can divide logic and have a truly decoupled code as well as code that is reusable for multiple application. This also means that we can very easily change some part of the application without concerning the other parts that much. For instance we can have an application that make orders and a different application that keeps track of users. We make an authenticate request to the users application when we need to authenticate a user. If we at some point decide to switch to a different platform for authentication, we will just build a small project that authenticates in a different way. The main application will then just make authentication request to the new project instead of the old one. 
### HTTPClient 🔹
In order for applications of different types to communicate with an API they need to communicate on the same language. That language is the HTTP protocol. This is why applications that do not come with features for communication of these kind out of the box, need to use a system for making HTTP requests and get responses. A HTTP Client is a system that allows an application to communicate through HTTP. The .NET library already has an implementation for a HTTP Client and it can be easily configured and used for making HTTP Requests.
#### Creating and using a HTTP Client 
```csharp asp
// Creating the HTTP Client
HttpClient client = new HttpClient();
// Setting URL
string url = "www.myapi.com/api/users";
// Making the call and getting response
HttpResponseMessage response = client.GetAsync(url).Result;
// Getting the response body from the response
string responseBody = response.Content.ReadAsStringAsync().Result;
```
### Another API 🔹
APIs can communicate with anything as we mentioned before. That means that they can communicate with different APIs as well. In microservice structure it is common for API application to communicate with each other creating a network of applications that just request things from each other. The only thing needed for this to work is for the APIs to know the addresses of the other APIs that they need to communicate and label them accordingly. This is usually done in the configuration file ( appsettings.json )
### Console Application 🔹
Console applications can also be connected to an API. They connect to APIs by using the .NET HTTP Client. With that they can make request to APIs and gather data as well as act as simple UI for the developers to perform some actions. 
### Front-End 🔹
Most common for web development is to connect a front-end application top an API. The front end application can be built in any library and system that can send HTTP requests. This is why web applications using javascript can easily connect to APIs and use the data to populate and dynamically change the web-site. Some JavaScript Libraries have their own HTTP Clients and systems for easier communication with APIs. The only thing to keep track when building a front-end application that will depend on an API is what the API sends and receives. 

## Extra Materials 📘
* [SeriLog Writing Logs](https://github.com/serilog/serilog/wiki/Writing-Log-Events)
* [SeriLog Configuration Documentation](https://github.com/serilog/serilog/wiki/Configuration-Basics)
* [Logging in .NET Best Practices](https://michaelscodingspot.com/logging-in-dotnet/)
* [Regex 101](https://regex101.com/)
