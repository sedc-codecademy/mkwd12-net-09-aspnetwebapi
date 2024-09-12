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
            //return _notesAppDbContext.Note
            //    .Include(x => x.User) // Join Notes table with Users table
            //    .ToList();

            // ===> EF behind scenes
            // Form the query in C#
            var query = _notesAppDbContext.Note.Include(x => x.User);

            // Way to check your LINQ to SQL translation
            var translatedQuery = query.ToQueryString();

            // When calling ToList() (executable method) our query is translated into SQL query => executed in the database => retrives data
            var materializedQuery = query.ToList();
            // EntityFramework Executable methods: ToList(), FirstOrDefault(), Any(), SingleOrDefault(), Count()...

            return materializedQuery;
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
