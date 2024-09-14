namespace Qinshift.NotesAppRefactored.Shared.CustomExceptions.FruitExceptions
{
    public class FruitNotFoundException : Exception
    {
        public FruitNotFoundException(string message) : base(message) { }
    }
}
