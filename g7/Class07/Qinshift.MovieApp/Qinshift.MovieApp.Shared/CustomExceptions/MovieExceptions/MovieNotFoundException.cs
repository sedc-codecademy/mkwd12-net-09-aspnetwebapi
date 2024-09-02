namespace Qinshift.MovieApp.Shared.CustomExceptions.MovieExceptions
{
    public class MovieNotFoundException : Exception
    {
        public MovieNotFoundException(string message) : base(message) { }
    }
}
