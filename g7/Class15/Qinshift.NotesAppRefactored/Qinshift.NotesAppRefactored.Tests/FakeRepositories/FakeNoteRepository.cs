using Qinshift.NotesAppRefactored.Data;
using Qinshift.NotesAppRefactored.Domain.Models;

namespace Qinshift.NotesAppRefactored.Tests.FakeRepositories
{
    public class FakeNoteRepository : IRepository<Note>
    {

        private List<Note> _notes;
        public FakeNoteRepository()
        {
            _notes = new List<Note>()
            {
                new Note()
                {
                    Id = 1,
                    UserId = 1,
                    Priority = Domain.Enums.Priority.High,
                    Tag = Domain.Enums.Tag.Health,
                    Text = "Do something",
                    User = new User
                    {
                        Id = 1,
                        FirstName = "Bob",
                        LastName = "Bobsky",
                        Age = 22,
                        Username = "Boby_123"
                    }
                },
                new Note()
                {
                    Id = 2,
                    UserId = 1,
                    Priority = Domain.Enums.Priority.Medium,
                    Tag = Domain.Enums.Tag.SocialLife,
                    Text = "Do something else",
                    User = new User
                    {
                        Id = 1,
                        FirstName = "Bob",
                        LastName = "Bobsky",
                        Age = 22,
                        Username = "Boby_123"
                    }
                }
            };
        }

        public void Add(Note entity)
        {
            _notes.Add(entity);
        }

        public void Delete(Note entity)
        {
            _notes.Remove(entity);
        }

        public List<Note> GetAll()
        {
            return _notes;
        }

        public Note GetById(int id)
        {
            return _notes.SingleOrDefault(note => note.Id == id);
        }

        public void Update(Note entity)
        {
            _notes[_notes.IndexOf(entity)] = entity;
        }
    }
}
