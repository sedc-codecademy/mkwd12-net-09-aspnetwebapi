using NotesAndTagsApp.Models;
using NotesAndTagsApp.Models.Enums;

namespace NotesAndTagsApp
{
    public static class StaticDb
    {
        public static List<User> Users = new List<User>()
        {
            new User
            {
                Id =1,
                FirstName = "Tijana",
                LastName = "Stojanovska",
                Username = "t.stojanovska",
                Password = "Test123"
            },

           new User
            {
                Id = 2,
                FirstName = "Roze",
                LastName = "Dobrinova",
                Username = "roze.d",
                Password = "123Test"
            }
        };

        public static List<Tag> Tags = new List<Tag>()
        {
            new Tag() { Id = 1, Name = "Homework", Color = "red"},
            new Tag() { Id = 2, Name = "SEDC", Color = "blue"},
            new Tag() { Id = 3, Name = "Health", Color = "green"},
            new Tag() { Id = 4, Name = "Exercise", Color = "white"},
            new Tag() { Id = 5, Name = "Fit", Color = "yellow"}
        };

        public static List<Note> Notes = new List<Note>()
        {
            new Note()
            {
                Id = 1,
                Text = "Do the homework",
                Priority = PriorityEnum.Medium,
                Tags = new List<Tag>
                {
                    new Tag() { Name = "Homework", Color = "blue"},
                    new Tag() { Name= "SEDC", Color = "purple"}
                },
                User = Users.First(),
                UserId = Users.First().Id
            },

            new Note()
            {
                Id =2,
                Text = "Drink more water",
                Priority = PriorityEnum.High,
                Tags = new List<Tag>
                {
                    new Tag() { Name = "Health", Color = "green"},
                },
                User = Users.First(),
                UserId = Users.First().Id
            },

            new Note()
            {
                Id =3,
                Text = "Go to the gym",
                Priority = PriorityEnum.Low,
                Tags = new List<Tag>
                {
                    new Tag() { Name = "Health", Color = "green"},
                    new Tag() { Name = "Exercise", Color = "red"},
                },
                User = Users.Last(),
                UserId = Users.Last().Id
            },
        };
    }
}
