using NotesApp.DataAccess;
using NotesApp.Domain.Enums;
using NotesApp.Domain.Models;

namespace NotesApp.Tests.FakeRepositories
{
    public class FakeNoteRepository : IRepository<Note>
    {
        private List<Note> _notes;

        public FakeNoteRepository()
        {
            var user = new User
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Bobsky",
                Age = 23,
                Username = "bob123"
            };

            _notes = new List<Note>()
            {
                new()
                {
                    Id = 1,
                    UserId = user.Id,
                    User = user,
                    Priority = Priority.High,
                    Tag = Tag.Health,
                    Text = "Do something"
                },
                new()
                {
                    Id = 2,
                    UserId = user.Id,
                    User = user,
                    Priority = Priority.Medium,
                    Tag = Tag.SocialLife,
                    Text = "Do something else!"
                }
            };
        }

        public List<Note> GetAll()
        {
            return _notes;
        }

        public Note GetById(int id)
        {
            return _notes.FirstOrDefault(n => n.Id == id);
        }

        public void Add(Note entity)
        {
            _notes.Add(entity);
        }

        public void Update(Note entity)
        {
            _notes[_notes.IndexOf(entity)] = entity;
        }

        public void Delete(Note entity)
        {
            _notes.Remove(entity);
        }

    }
}
