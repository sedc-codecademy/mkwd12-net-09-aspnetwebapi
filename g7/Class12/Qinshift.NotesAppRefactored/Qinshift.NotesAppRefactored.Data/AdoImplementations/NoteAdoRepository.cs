using Microsoft.Data.SqlClient;
using Qinshift.NotesAppRefactored.Domain.Enums;
using Qinshift.NotesAppRefactored.Domain.Models;

namespace Qinshift.NotesAppRefactored.Data.AdoImplementations
{
    public class NoteAdoRepository : IRepository<Note>
    {
        private readonly string _connectionString;

        public NoteAdoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Note entity)
        {
            //1. Create new connection to our SQL database
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            //2. Open the connection
            sqlConnection.Open();
            //3. Create sql command
            SqlCommand command = new SqlCommand();
            //4. connect the command
            command.Connection = sqlConnection;

            //5. binding the values for the specific table and the specific columns
            command.CommandText = "INSERT INTO dbo.Notes(Text, Priority, Tag, UserId)" +
                                  "VALUES(@text, @priority, @tagm @userId)"; // values come from outside(from the client) thats why we use parameters
            
            command.Parameters.AddWithValue("@text", entity.Text);
            command.Parameters.AddWithValue("@priority", entity.Priority);
            command.Parameters.AddWithValue("@tag", entity.Tag);
            command.Parameters.AddWithValue("@userId", entity.UserId);

            //BAD EXAMPLE - potention sql injecttion attack 
            //command.CommandText = "INSERT INTO dbo.Notes(Text, Priority, Tag, UserId)" +
            //                      $"VALUES({entity.Text}, @priority, @tag, userId)";

            //6. executing
            command.ExecuteNonQuery();
            //7. close the connection
            sqlConnection.Close();
        }

        public void Delete(Note entity)
        {
            //1. creating connection
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            //2. open the created connection
            sqlConnection.Open();
            //3. create sql command
            SqlCommand command = new SqlCommand();
            //4. connect the command
            command.Connection = sqlConnection;

            //5. we find the id provided and delete the record with that given id
            command.CommandText = "DELETE FROM dbo.Notes WHERE Id = @id";
            command.Parameters.AddWithValue("@id", entity.Id);

            //6 executing
            command.ExecuteNonQuery();

            //7 close the connection
            sqlConnection.Close();
        }

        public List<Note> GetAll()
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = "SELECT * FROM dbo.Notes";

            List<Note> notesDb = new List<Note>();

            SqlDataReader sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {
                notesDb.Add(new Note()
                {
                    Id = (int)sqlDataReader["ID"],
                    Text = (string)sqlDataReader["Text"],
                    Priority = (Priority)sqlDataReader["Priority"],
                    Tag = (Tag)sqlDataReader["Tag"],
                    UserId = (int)sqlDataReader["UserId"],
                    //User = new User
                    //{
                    //    FirstName = (string)sqlDataReader["FirstName"]
                    //}
                });
            }

            sqlConnection.Close();
            return notesDb;
        }

        public Note GetById(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = "SELECT * FROM dbo.Notes WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);

            List<Note> notesDb = new List<Note>();

            SqlDataReader sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {
                notesDb.Add(new Note()
                {
                    Id = (int)sqlDataReader["ID"],
                    Text = (string)sqlDataReader["Text"],
                    Priority = (Priority)sqlDataReader["Priority"],
                    Tag = (Tag)sqlDataReader["Tag"],
                    UserId = (int)sqlDataReader["UserId"],
                    //User = new User
                    //{
                    //    FirstName = (string)sqlDataReader["FirstName"]
                    //}
                });
            }

            sqlConnection.Close();
            return notesDb.FirstOrDefault();
        }

        public void Update(Note entity)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;

            command.CommandText = "UPDATE dbo.Notes SET Text @text, Tag = @tag, Priority = @priority, UserId = @userId" +
                                  "WHERE Id = @id";

            command.Parameters.AddWithValue("@text", entity.Text);
            command.Parameters.AddWithValue("@priority", entity.Priority);
            command.Parameters.AddWithValue("@tag", entity.Tag);
            command.Parameters.AddWithValue("@userId", entity.UserId);
            command.Parameters.AddWithValue("@id", entity.Id);

            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
