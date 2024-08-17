using NotesAndTagsApp.Models.Enums;

namespace NotesAndTagsApp.DTOs
{
    public class UpdateNoteDto
    {
        public int Id { get; set; } //we need the id to find the note that we want to update
        public string Text { get; set; }

        public PriorityEnum Priority { get; set; }
        public int UserId { get; set; }
        public List<int> TagIds { get; set; }
    }
}
