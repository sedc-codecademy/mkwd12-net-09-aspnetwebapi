using Qinshift.NotesAppRefactored.Domain.Enums;

namespace Qinshift.NotesAppRefactored.Dtos.NoteDtos
{
    public class NoteDto
    {
        public string Text { get; set; }
        public Priority priority { get; set; }
        public Tag Tag { get; set; }
        public string UserFullName { get; set; }
    }
}
