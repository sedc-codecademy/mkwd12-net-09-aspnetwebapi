namespace NotesApp.Shared.CustomException
{
    public class NoteNotFoundException : Exception
    {
        public NoteNotFoundException(string message) : base(message)
        {

        }
    }
}
