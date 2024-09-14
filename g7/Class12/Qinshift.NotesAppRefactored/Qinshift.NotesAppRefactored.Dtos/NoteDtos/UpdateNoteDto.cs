using Qinshift.NotesAppRefactored.Domain.Enums;

namespace Qinshift.NotesAppRefactored.Dtos.NoteDtos
{
    public class UpdateNoteDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public Tag Tag { get; set; }
        public int UserId { get; set; }
    }
}
