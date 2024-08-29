using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NotesApp.Domain.Models;

namespace NotesApp.DataAccess.Implementations.DapperImplementation
{
    public class NoteDapperRepository : IRepository<Note>
    {
        private readonly string _connectionString;

        public NoteDapperRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("NotesAppSqlExpress");
        }

        public List<Note> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM Note n 
                                 JOIN [User] u ON n.UserId = u.Id";

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
            using SqlConnection connection = new SqlConnection(_connectionString);
            string query = @"SELECT * FROM Note n 
                             JOIN [User] u ON n.UserId = u.Id 
                             WHERE n.Id = @Id";

            var note = connection.Query<Note, User, Note>(query, (note, user) =>
            {
                note.User = user;
                return note;
            },
            new { Id = id },
            splitOn: "UserId").FirstOrDefault();

            return note;
        }

        public void Add(Note entity)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            //string query = "INSERT INTO dbo.Note (Text, Priority, Tag, UserId) " +
            //                "VALUES (@Text, @Priority, @Tag, @UserId)";
            string query = "EXEC sp_AddNote @Text, @Priority, @Tag, @UserId";
            connection.Execute(query, entity);
        }

        public void Update(Note entity)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            string sql = @"UPDATE dbo.Note
                           SET Text = @Text, Priority = @Priority, Tag = @Tag, UserId = @UserId
                           WHERE Id = @Id";
            connection.Execute(sql, entity);
        }

        public void Delete(Note entity)
        {
            // NOTE: in real case scenario we rarely perform HARD delete to records in our Database, instead we use SOFT delete (ex. set model's property IsDeleted to true)
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Execute("DELETE FROM Note WHERE Id = @noteId", new { noteId = entity.Id });
        }

    }
}
