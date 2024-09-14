using NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.DataAccess
{
    public interface IRepository<T> where T : BaseEntity
    {
        //CRUD
        List<T> GetAll();
        T GetById(int id);
        void Delete(T entity);
        void Add(T entity);
        void Update(T entity);
    }
}
