using Microsoft.EntityFrameworkCore;
using NotesApp.DataAccess.Context;
using NotesApp.Domain.Models;

namespace NotesApp.DataAccess.Implementations.EFImplementation
{
    public class NoteRepository : IRepository<Note>
    {
        private readonly NotesAppDbContext _notesAppDbContext;

        public NoteRepository(NotesAppDbContext notesAppDbContext)
        {
            _notesAppDbContext = notesAppDbContext;
        }

        public List<Note> GetAll()
        {
            return _notesAppDbContext.Note
                .Include(x => x.User) // Join Notes table with Users table
                .ToList();
        }

        public Note GetById(int id)
        {
            return _notesAppDbContext.Note.Include(x => x.User).FirstOrDefault(x => x.Id == id);
        }

        public void Add(Note entity)
        {
            _notesAppDbContext.Note.Add(entity);
            _notesAppDbContext.SaveChanges();
        }

        public void Update(Note entity)
        {
            _notesAppDbContext.Note.Update(entity);
            _notesAppDbContext.SaveChanges();
        }

        public void Delete(Note entity)
        {
            _notesAppDbContext.Note.Remove(entity);
            _notesAppDbContext.SaveChanges();
        }

    }
}
