using Microsoft.EntityFrameworkCore;
using Qinshift.NotesAppRefactored.Domain.Models;

namespace Qinshift.NotesAppRefactored.Data.Implementations
{
    public class NoteRepository : IRepository<Note>
    {

        private readonly NotesAppDbContext _notesAppDbContext;

        public NoteRepository(NotesAppDbContext notesAppDbContext)
        {
            _notesAppDbContext = notesAppDbContext;
        }

        public void Add(Note entity)
        {
            _notesAppDbContext.Notes.Add(entity);
            _notesAppDbContext.SaveChanges(); // where call to db happens
        }

        public void Delete(Note entity)
        {
            _notesAppDbContext.Notes.Remove(entity);
            _notesAppDbContext.SaveChanges(); // where call to db happens
        }

        public List<Note> GetAll()
        {
            return _notesAppDbContext.Notes
                .Include(x => x.User) // this will =>  join notes table with users table and we will get the notes's user info aswell
                .ToList();
        }

        public Note GetById(int id)
        {
            return _notesAppDbContext.Notes
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Note entity)
        {
            _notesAppDbContext.Notes.Update(entity);
            _notesAppDbContext.SaveChanges(); // where call to db happens
        }
    }
}
