using Microsoft.EntityFrameworkCore;
using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTAgsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.DataAccess.Implementation
{
    public class NoteRepository : IRepository<Note>
    {
        private NotesAndTagsAppDbContext _dbContext;

        public NoteRepository(NotesAndTagsAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Note entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Note entity)
        {
            throw new NotImplementedException();
        }

        public List<Note> GetAll()
        {
            return _dbContext
                .Notes
                .Include(x => x.User) //join Notes table with users table, to be able to access the properties of user via note
                .ToList();
        }

        public Note GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}
