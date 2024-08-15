using Qinshift.NotesApp.Models.Enums;
using Qinshift.NotesApp.Models;

namespace Qinshift.NotesApp
{
    public class StaticDb
    {
        public static List<User> Users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Bobsky",
                Username = "bbobsky",
                Password = "bbobsky123"
            },
            new User
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe",
                Username = "jdoe",
                Password = "jdoe123"
            }
        };

        public static List<Tag> Tags = new List<Tag>
        {
                new Tag(){ Id = 1, Name = "Exercise", Color = "blue"},
                new Tag(){ Id = 2, Name = "Work", Color = "green"},
                new Tag(){ Id = 3, Name = "Study", Color = "purple"},
                new Tag(){ Id = 4, Name = "Health", Color = "darkred"},
                new Tag(){ Id = 5, Name = "Shopping", Color = "pink"},
                new Tag(){ Id = 6, Name = "Travel", Color = "magenta"},
                new Tag(){ Id = 7, Name = "Urgent", Color = "red"},
                new Tag(){ Id = 8, Name = "Reading", Color = "lightblue"}
        };

        public static List<Note> Notes = new List<Note>()
        {
            new Note
            {
                Id = 1,
                Text = "Do Homework",
                Priority = Priority.Low,
                Tags = new List<Tag>{ Tags[2], Tags[6] },
                User = Users.First(),
                UserId = Users.First().Id
            },
            new Note
            {
                Id = 2,
                Text = "Drink more Water",
                Priority = Priority.High,
                Tags = new List<Tag> { Tags[3] },
                User = Users.First(),
                UserId = Users.First().Id
            },
            new Note
            {
                Id = 3,
                Text = "Go to the gym",
                Priority = Priority.Medium,
                Tags = new List<Tag>() { Tags[0] },
                User = Users.Last(),
                UserId = Users.Last().Id
            }
        };

    }
}
