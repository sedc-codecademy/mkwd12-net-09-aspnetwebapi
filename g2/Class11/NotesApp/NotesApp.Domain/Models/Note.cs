using NotesApp.Domain.Enums;

namespace NotesApp.Domain.Models
{
    public class Note : BaseEntity
    {
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public Tag Tag { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
