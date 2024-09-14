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
            using(SqlConnection sqlConnection = new SqlConnection(_connectionString)) { 
                sqlConnection.Open();

                string query = "INSERT INTO dbo.Notes(Text, Priority, Tag, UserId)" +
                                  "VALUES(@text, @priority, @tag, @userId)";

                sqlConnection.Execute(query, new //anonymous object where we send our params as key-value
                {
                    text = entity.Text,
                    priority = entity.Priority,
                    tag = entity.Tag,
                    userId = entity.UserId
                });
            }
        }

        public void Delete(Note entity)
        {
            using(SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string deleteQuery = "DELETE FROM dbo.Notes WHERE Id = @id";

                sqlConnection.Execute(deleteQuery, new {id =  entity.Id});
            }
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
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM dbo.Notes WHERE Id = " + id;

                var notes = connection.Query<Note>(query);

                return notes.FirstOrDefault();
            }
            }

        public void Update(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}
