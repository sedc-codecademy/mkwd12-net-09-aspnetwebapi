using NotesAndTagsApp.Models;
using NotesAndTagsApp.Models.Enums;

namespace NotesAndTagsApp
{
    public static class StaticDb
    {
        public static List<Note> Notes = new List<Note>()
        {
            new Note()
            {
                Text = "Do the homework",
                Priority = PriorityEnum.Medium,
                Tags = new List<Tag>
                {
                    new Tag() { Name = "Homework", Color = "blue"},
                    new Tag() { Name= "SEDC", Color = "purple"}
                }
            },

            new Note()
            {
                Text = "Drink more water",
                Priority = PriorityEnum.High,
                Tags = new List<Tag>
                {
                    new Tag() { Name = "Health", Color = "green"},
                }
            },

            new Note()
            {
                Text = "Go to the gym",
                Priority = PriorityEnum.Low,
                Tags = new List<Tag>
                {
                    new Tag() { Name = "Health", Color = "green"},
                    new Tag() { Name = "Exercise", Color = "red"},
                }
            },
        };
    }
}
