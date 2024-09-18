namespace NotesAppConsoleClient.Models
{
    // Models that our Console Client uses (similar/same to those from our api)
    public class NoteResponse
    {
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public Tag Tag { get; set; }
        public string UserFullName { get; set; }

        public override string ToString()
        {
            return $"\n*Note text: {Text}. *Priority: {Priority}. *Tag: {Tag}. *User: {UserFullName}";
        }
    }

    public enum Priority
    {
        Low = 1,
        Medium,
        High
    }

    public enum Tag
    {
        Work = 1,
        Health,
        SocialLife
    }
}
