# Connecting API to other services 🚁

## Connecting other applications to our API 🔶

### Microservice structure 🔹

Microservice structure and building of projects is a revolutionary way of building applications. It is a very simple concept, instead of building the whole logic inside of one application we can create multiple small applications that will do a certain thing really well independently. After the applications are built they will communicate with each other to create one big implementation and work as one application. This might sound strange, but with this implementation, we can divide logic and have a truly decoupled code as well as code that is reusable for multiple applications. This also means that we can very easily change some parts of the application without concerning the other parts that much. For instance, we can have an application that makes orders and a different application that keeps track of users. We make an authenticated request to the user's application when we need to authenticate a user. If we at some point decide to switch to a different platform for authentication, we will just build a small project that authenticates in a different way. The main application will then just make an authentication request to the new project instead of the old one.

### HTTPClient 🔹

In order for applications of different types to communicate with an API, they need to communicate in the same language. That language is the HTTP protocol. This is why applications that do not come with features for communication of this kind out of the box, need to use a system for making HTTP requests and getting responses. An HTTP Client is a system that allows an application to communicate through HTTP. The .NET library already has an implementation for an HTTP Client and it can be easily configured and used for making HTTP Requests.

#### Creating and using an HTTP Client

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

APIs can communicate with anything as we mentioned before. That means that they can communicate with different APIs as well. In microservice structure, it is common for API applications to communicate with each other creating a network of applications that just request things from each other. The only thing needed for this to work is for the APIs to know the addresses of the other APIs that they need to communicate and label them accordingly. This is usually done in the configuration file ( appsettings.json )

### Console Application 🔹

Console applications can also be connected to an API. They connect to APIs by using the .NET HTTP Client. With that, they can make a request to APIs and gather data as well as act as simple UI for the developers to perform some actions.

### Front-End 🔹

The most common for web development is to connect a front-end application to an API. The front-end application can be built in any library and system that can send HTTP requests. This is why web applications using javascript can easily connect to APIs and use the data to populate and dynamically change the website. Some JavaScript Libraries have their own HTTP Clients and systems for easier communication with APIs. The only thing to keep track of when building a front-end application that will depend on an API is what the API sends and receives.

## Extra Materials 📘

- [Regex 101](https://regex101.com/)
