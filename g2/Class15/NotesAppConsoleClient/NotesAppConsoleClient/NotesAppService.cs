using Newtonsoft.Json;
using NotesAppConsoleClient.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace NotesAppConsoleClient
{
    public class NotesAppService
    {
        private readonly string _baseUrl = "https://localhost:7033"; // NOTE : Enter the correct port when testing
        private string? _token;

        public NotesAppService()
        {
            
        }

        public async Task UserLoginAsync(string username, string password)
        {
            
        }

        public async Task GetNotesAsync()
        {
           
        }
    }

}
