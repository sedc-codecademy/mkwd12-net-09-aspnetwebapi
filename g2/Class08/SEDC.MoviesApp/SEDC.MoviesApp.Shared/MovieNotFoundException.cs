namespace SEDC.MoviesApp.Shared
{
    public class MovieNotFoundException : Exception
    {
        public MovieNotFoundException(string message) : base(message)
        {

        }
    }
}
