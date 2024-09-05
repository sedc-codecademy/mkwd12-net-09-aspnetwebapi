using NotesApp.Domain.Enums;

namespace NotesApp.Dto.NoteDto
{
    public class AddNoteDto
    {
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public Tag Tag { get; set; }
        public int UserId { get; set; }
    }
}
