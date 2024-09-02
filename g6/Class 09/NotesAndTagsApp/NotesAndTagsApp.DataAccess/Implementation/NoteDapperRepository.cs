using Dapper;
using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTAgsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.DataAccess.Implementation
{
    public class NoteDapperRepository : IRepository<Note>
    {
        private string _connectionString;

        public NoteDapperRepository(string connectionString)
        {
            _connectionString = connectionString;
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
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM dbo.Notes N INNER JOIN dbo.Users U ON U.Id = N.UserId";

                var notes = connection.Query<Note, User, Note>(query, (note, user) =>
                {
                    note.User = user;
                    return note;
                },
                splitOn: "UserId");

                return notes.ToList();
            }
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
