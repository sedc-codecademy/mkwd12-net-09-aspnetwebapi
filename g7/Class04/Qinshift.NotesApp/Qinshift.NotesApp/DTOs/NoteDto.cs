using Qinshift.NotesApp.Models.Enums;

namespace Qinshift.NotesApp.DTOs
{
    public class NoteDto
    {
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public string User {  get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}
