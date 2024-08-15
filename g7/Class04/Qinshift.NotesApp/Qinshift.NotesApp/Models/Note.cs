using Qinshift.NotesApp.Models.Enums;

namespace Qinshift.NotesApp.Models
{
    public class Note : BaseEntity
    {
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();

        public Note()
        {
            // Tags = new List<Tag>(); // leave the constructor for more important operations
        }
    }
}
