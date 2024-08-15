using Qinshift.NotesApp.Models.Enums;

namespace Qinshift.NotesApp.DTOs
{
    public class UpdateNoteDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public int UserId { get; set; }
    }
}
