using Newtonsoft.Json;
using NotesAppConsoleClient.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace NotesAppConsoleClient
{
    public class NotesAppService
    {
        private HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7033"; // NOTE : Enter the correct port when testing
        private string? _token;

        public NotesAppService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_baseUrl)
            };
        }

        public async Task UserLoginAsync(string username, string password)
        {
            // Create login dto
            var loginRequestDto = new LoginRequest(username, password);

            // Serialize the login request object to JSON
            // Send the POST request to the login endpoint
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/User/login", loginRequestDto);

            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                _token = responseBody;
            }
            else
            {
                throw new Exception("\n\n\tInvalid login attempt!");
            }
        }

        public async Task GetNotesAsync()
        {
            // Ensure that a token is available
            if (string.IsNullOrEmpty(_token))
            {
                throw new Exception("Authentication token is missing. Please log in first.");
            }

            // Add the Authorization header with the token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // Send the GET request to the notes endpoint
            HttpResponseMessage response = await _httpClient.GetAsync("/api/notes");

            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response content into a list of notes
                string responseBody = await response.Content.ReadAsStringAsync();
                List<NoteResponse> notes = JsonConvert.DeserializeObject<List<NoteResponse>>(responseBody);

                // Print notes if any
                notes?.PrintNotes();
            }
            else
            {
                throw new Exception("Notes request failed!");
            }
        }
    }

}
