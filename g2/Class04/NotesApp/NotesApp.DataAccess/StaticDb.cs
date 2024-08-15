using NotesApp.Domain.Enums;
using NotesApp.Domain.Models;

namespace NotesApp.DataAccess
{
    public static class StaticDb
    {
        public static int NoteId = 3;

        public static List<User> Users = new List<User>
        {
            new User()
            {
                Id = 1,
                FirstName = "Smith",
                LastName = "Smithsky",
                Username = "ssmith",
                Password = "1234567890"
            },
            new User()
            {
                Id = 2,
                FirstName = "Jill",
                LastName = "Jillsky",
                Username = "jjllsky",
                Password = "123456789"
            }
        };

        public static List<Tag> Tags = new List<Tag>()
        {
            new Tag(){Id = 1,Name = "Homework", Color = "Red"},
            new Tag(){Id = 2,Name = "Qinshift", Color = "Green"},
            new Tag(){Id = 3,Name = "Healthy", Color = "Blue"},
            new Tag(){Id = 4,Name = "Water", Color = "Green"},
            new Tag(){Id = 5,Name = "Exercise", Color = "Red"},
            new Tag(){Id = 6,Name = "Fit", Color = "Blue"},
            new Tag(){Id = 7,Name = "Food", Color = "Green"}
        };

        public static List<Note> Notes = new List<Note>()
        {
            new Note(){
                Id = 1, 
                Text = "Do Homework", 
                Priority = Priority.High, 
                Tags = new List<Tag>()
                {
                    new Tag(){Id = 1,Name = "Homework", Color = "Red"},
                    new Tag(){Id = 2,Name = "Qinshift", Color = "Green"}
                },
                User = Users.First(),
                UserId = Users.First().Id
            },
             new Note(){
                Id = 2,
                Text = "Drink more water",
                Priority = Priority.Medium,
                Tags = new List<Tag>()
                {
                    new Tag(){Id = 3,Name = "Healthy", Color = "Blue"},
                    new Tag(){Id = 4,Name = "Water", Color = "Green"}
                },
                User = Users.First(),
                UserId = Users.First().Id
            },
             new Note(){
                Id = 3,
                Text = "Go to the gym",
                Priority = Priority.Low,
                Tags = new List<Tag>()
                {
                    new Tag(){Id = 5,Name = "Exercise", Color = "Red"},
                    new Tag(){Id = 6,Name = "Fit", Color = "Blue"},
                    new Tag(){Id = 7,Name = "Food", Color = "Green"}
                },
                User = Users.Last(),
                UserId = Users.Last().Id
            }
        };
    }
}
