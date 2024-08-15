using NotesAndTagsApp.Models.Enums;

namespace NotesAndTagsApp.DTOs
{
    //DTO == Data Transfer Object
    public class NoteDto
    {
        public string Text { get; set; }
        public string User { get; set; }
        public PriorityEnum Priority { get; set; }

        public List<string> Tags { get; set; }
    }
}
