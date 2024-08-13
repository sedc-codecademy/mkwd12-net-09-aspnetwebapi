namespace Qinshift.NotesAppAndTags.Models
{
    public class Note
    {
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
