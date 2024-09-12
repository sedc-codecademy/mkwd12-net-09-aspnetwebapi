namespace Qinshift.NotesAppRefactored.Shared.CustomExceptions.NoteExceptions
{
    public class NoteNotFoundException : Exception
    {
        public NoteNotFoundException(string message) : base(message) { }
    }
}
