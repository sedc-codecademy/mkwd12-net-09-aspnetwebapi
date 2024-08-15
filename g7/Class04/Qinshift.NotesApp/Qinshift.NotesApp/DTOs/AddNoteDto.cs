using Qinshift.NotesApp.Models.Enums;

namespace Qinshift.NotesApp.DTOs
{
    public class AddNoteDto
    {
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public int UserId { get; set; }
        public List<int> TagIds { get; set; } = new List<int>();
    }
}
