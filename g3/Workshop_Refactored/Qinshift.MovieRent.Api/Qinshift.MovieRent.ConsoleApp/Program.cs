using Newtonsoft.Json;
using Qinshift.MovieRent.ConsoleApp.Models;
using Qinshift.MovieRent.DTOs;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Qinshift.MovieRent.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string url = "https://jsonplaceholder.typicode.com/todos";
            var todoList = GetAllTodos(url);
            PrintTodos(todoList);

            Thread.Sleep(5000);
            

            Console.WriteLine("====================== MOVIES ==============================");
            string moviesUrl = "https://localhost:7156/api/movies/all";
            var movies = GetAllMovies(moviesUrl);
            PrintMovies(movies);


            Console.Read();
        }

        static List<Todo> GetAllTodos(string url)
        {
            using (HttpClient _client = new HttpClient())
            {
                HttpResponseMessage response = _client.GetAsync(url).Result;
                List<Todo> todos =  JsonConvert
                    .DeserializeObject<List<Todo>>(response.Content.ReadAsStringAsync().Result);
                return todos;
            }
        }

        static List<MovieDto> GetAllMovies(string url)
        {
            using (HttpClient _client = new HttpClient())
            {
                // This is how you add authorization header in httpClient in C#
                //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
                HttpResponseMessage response = _client.GetAsync(url).Result;
                List<MovieDto> movies = JsonConvert
                    .DeserializeObject<List<MovieDto>>(response.Content.ReadAsStringAsync().Result);
                return movies;
            }
        }

        static void PrintTodos(List<Todo> todos)
        {
            foreach (var todo in todos)
            {
                Console.WriteLine($"{todo.Id}. {todo.Title} | Status: {(todo.Completed ? "Completed" : "Incomplete")}");
            }
        }

        static void PrintMovies(List<MovieDto> movies)
        {
            foreach (var movie in movies)
            {
                Console.WriteLine($"{movie.Title} | Genre: {movie.Genre} | Year: {movie.ReleaseDate.ToShortDateString()}");
            }
        }
    }
}
