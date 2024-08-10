using Qinshift.NotesAppAndTags.Models;

namespace Qinshift.NotesAppAndTags.Db
{
    public static class StaticDb
    {
        public static List<string> Users = new List<string>()
        {
            "Slave",
            "Martin",
            "Bob",
            "Jill",
            "Jayne"
        };

        public static List<Note> Notes = new List<Note>()
        {
            new Note() { Text = "Do homework!", Priority = Priority.High, Tags = new List<Tag>()
            {
                new Tag() {Name = "School homework", Color = "Red"},
                new Tag() {Name = "Qinshift academy homework", Color = "Orange"},
            }
            },
            new Note() { Text = "Drink more water!", Priority = Priority.Medium, Tags = new List<Tag>()
            {
                new Tag() {Name = "Healthy", Color = "Green"},
                new Tag() {Name = "Priority High", Color = "Red"},
            }
            },
            new Note() { Text = "Go to the gym", Priority = Priority.Low, Tags = new List<Tag>()
            {
                new Tag() {Name = "Lift weights", Color = "Red"},
                new Tag() {Name = "Do cardio", Color = "Blue"},
            }
            }
        };
    }
}
