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
            _dbContext.Notes.Add(entity); //here we have not called the db yet, so the changes aren't saved
            _dbContext.SaveChanges();//call to db
        }

        public void Delete(Note entity)
        {
           _dbContext.Notes.Remove(entity);
           _dbContext.SaveChanges();
        }

        public List<Note> GetAll()
        {
            return _dbContext
                .Notes
                .Include(x => x.User) //join Notes table with users table, to be able to access the properties of user via note
                .ToList();

            //select * from Notes N
            //inner join Users U on N.UserId = U.Id
        }

        public Note GetById(int id)
        {
            return _dbContext
                    .Notes
                    .Include(x => x.User)
                    .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Note entity)
        {
           _dbContext.Notes.Update(entity);
           _dbContext.SaveChanges(); //call to db
        }
    }
}
