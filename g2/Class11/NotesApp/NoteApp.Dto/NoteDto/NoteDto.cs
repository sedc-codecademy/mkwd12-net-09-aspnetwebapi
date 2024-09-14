using NotesApp.Domain.Enums;

namespace NotesApp.Dto.NoteDto
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public Tag Tag { get; set; }
        public string UserFullName { get; set; }
    }
}
