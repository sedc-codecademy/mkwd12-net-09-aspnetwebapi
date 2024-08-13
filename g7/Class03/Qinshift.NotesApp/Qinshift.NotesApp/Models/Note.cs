using Qinshift.NotesApp.Models.Enums;

namespace Qinshift.NotesApp.Models
{
    public class Note
    {
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
