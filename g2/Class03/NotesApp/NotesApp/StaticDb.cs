using NotesApp.Model;
using NotesApp.Model.Enums;

namespace NotesApp
{
    public static class StaticDb
    {
        public static List<Note> Notes = new List<Note>()
        {
            new Note(){
                Id = 1,
                Text = "Do Homework", 
                Priority = Priority.High,
                Tags = new List<Tag>()
                {
                    new Tag()
                    {
                        Name = "Homework",
                        Color = "red"
                    },
                    new Tag()
                    {
                        Name = "SEDC",
                        Color = "red"
                    }
                }
            },
            new Note(){
                Id = 2,
                Text = "Drink more water",
                Priority = Priority.Medium,
                Tags = new List<Tag>()
                {
                    new Tag()
                    {
                        Name = "Healthy",
                        Color = "yellow"
                    }  
                }
            },
            new Note(){
                Id = 3,
                Text = "Go to the gym",
                Priority = Priority.Low,
                Tags = new List<Tag>()
                {
                    new Tag()
                    {
                        Name = "Exercise",
                        Color = "green"
                    }
                }
            }
        };
    }
}
