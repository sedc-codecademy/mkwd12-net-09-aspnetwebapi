using Qinshift.NotesApp.Models;
using Qinshift.NotesApp.Models.Enums;

namespace Qinshift.NotesApp
{
    public static class StaticDb
    {
        public static List<Note> Notes = new List<Note>()
        {
            new Note()
            {
                Text = "Do Homework",
                Priority = Priority.High,
                Tags = new List<Tag>
                {
                    new Tag { Name = "HomeWork", Color = "cyan" },
                    new Tag { Name = "Qinshift", Color = "blue" },
                }
            },
            new Note()
            {
                Text = "Drink more Water",
                Priority = Priority.Medium,
                Tags = new List<Tag>
                {
                    new Tag { Name = "Healthy", Color = "orange" },
                    new Tag { Name = "It's good for you", Color = "red" },
                }
            },
            new Note()
            {
                Text = "Go to the gym",
                Priority = Priority.Low,
                Tags = new List<Tag>
                {
                    new Tag { Name = "Exercise", Color = "blue" },
                }
            },
        };
    }
}
