using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NotesApp.Domain.Enums;
using NotesApp.Domain.Models;

namespace NotesApp.DataAccess.Implementations.AdoNetImplementation
{
    public class NoteAdoNetRepository : IRepository<Note>
    {
        private readonly string _connectionString;

        public NoteAdoNetRepository(IConfiguration configuration) // built-in Dependency Injection for our Configuration
        {
            //_connectionString = "Server=.\\SQLEXPRESS;Database=NotesAppDb;Trusted_Connection=True;Integrated Security=True;Encrypt=False;"; // BAD WAY !

            _connectionString = configuration.GetConnectionString("NotesAppSqlExpress");
        }

        // SqlConnection => used to establish connection to a database
        // SqlCommand => execute SQL queries, stored procedures, and other database commands
        // SqlDataReader => read data from a database

        public List<Note> GetAll()
        {
            var notes = new List<Note>();

            // 1) Establish the connection to the Database
            //SqlConnection sqlConnection = new SqlConnection(_connectionString);
            //sqlConnection.Open();
            //// .......
            //sqlConnection.Close(); // BAD WAY !!!
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                // Define the SQL query
                string query = @"SELECT n.Id, n.Text, n.Priority, n.Tag, n.UserId, u.FirstName, u.LastName
                                 FROM dbo.Note n
                                 JOIN dbo.[User] u ON n.UserId = u.Id";

                // 2. Create SQL command
                using SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // 3. Execute the sql command in the database
                using SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                // 4. Read the data from the executed query
                while (sqlDataReader.Read())
                {
                    // 5. Map manually the retrived properties to Note object
                    var note = new Note()
                    {
                        Id = sqlDataReader.GetInt32(0),
                        Text = sqlDataReader.GetString(1),
                        Priority = (Priority)sqlDataReader.GetInt32(2),
                        Tag = (Tag)sqlDataReader.GetInt32(3),
                        //UserId = (int)sqlDataReader["UserId"], // other way
                        UserId = sqlDataReader.GetInt32(4),
                        // if we want to take the whole user than we will need to JOIN the Users table, and then map the properties accordingly
                        User = new User
                        {
                            FirstName = sqlDataReader.GetString(5),
                            LastName = sqlDataReader.GetString(6),
                        }
                        // NOTE: The order of the columns is the one written in the SELECT query
                    };
                    notes.Add(note);
                }
            }
            return notes;
        }

        public Note GetById(int id)
        {
            // 1) Establish the connection to the Database
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                // Define the SQL query
                string query = @"SELECT n.Id, n.Text, n.Priority, n.Tag, n.UserId, u.FirstName, u.LastName
                                 FROM dbo.Note n
                                 JOIN dbo.[User] u ON n.UserId = u.Id
                                 WHERE n.Id = @NoteId";

                // 2. Create SQL command
                using SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@NoteId", id);

                // 3. Execute the sql command in the database
                using SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                // 4. Read the data from the executed query
                if (sqlDataReader.Read())
                {
                    var note = new Note
                    {
                        Id = sqlDataReader.GetInt32(0),
                        Text = sqlDataReader.GetString(1),
                        Priority = (Priority)sqlDataReader.GetInt32(2),
                        Tag = (Tag)sqlDataReader.GetInt32(3),
                        UserId = sqlDataReader.GetInt32(4),
                        User = new User
                        {
                            FirstName = sqlDataReader.GetString(5),
                            LastName = sqlDataReader.GetString(6),
                        }
                    };

                    return note;
                }
                return null;
            }
        }

        public void Add(Note entity)
        {
            // 1) Establish the connection to the Database
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                // Define the SQL query
                //string query = "INSERT INTO dbo.Note (Text, Priority, Tag, UserId) " +
                //               $"VALUES ({entity.Text}, {entity.Priority}, {entity.Tag}, {entity.UserId})";

                string query = "INSERT INTO dbo.Note (Text, Priority, Tag, UserId) " +
                               "VALUES (@Text, @Priority, @TagEnum, @UserId)";

                // 2. Create SQL command
                using SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Text", entity.Text);
                sqlCommand.Parameters.AddWithValue("@Priority", entity.Priority);
                sqlCommand.Parameters.AddWithValue("@TagEnum", entity.Tag);
                sqlCommand.Parameters.AddWithValue("@UserId", entity.UserId);

                sqlCommand.ExecuteNonQuery();
                //int rowsAffected = sqlCommand.ExecuteNonQuery();
            }
        }

        public void Update(Note entity)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"UPDATE dbo.Note
                             SET Text = @Text, Priority = @Priority, Tag = @Tag, UserId = @UserId
                             WHERE Id = @NoteId";

            using SqlCommand sqlCommand = new SqlCommand( query, connection);
            sqlCommand.Parameters.AddWithValue("@NoteId", entity.Id);
            sqlCommand.Parameters.AddWithValue("@Text", entity.Text);
            sqlCommand.Parameters.AddWithValue("@Priority", entity.Priority);
            sqlCommand.Parameters.AddWithValue("@Tag", entity.Tag);
            sqlCommand.Parameters.AddWithValue("@UserId", entity.UserId);

            sqlCommand.ExecuteNonQuery();
        }

        public void Delete(Note entity)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = "DELETE FROM dbo.Note WHERE Id = @noteId";
            using SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue("@noteId", entity.Id);

            sqlCommand.ExecuteNonQuery();
        }

    }
}
