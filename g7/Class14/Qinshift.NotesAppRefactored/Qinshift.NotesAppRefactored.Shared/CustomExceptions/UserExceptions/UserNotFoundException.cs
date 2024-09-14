namespace Qinshift.NotesAppRefactored.Shared.CustomExceptions.UserExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message) { }
    }
}
