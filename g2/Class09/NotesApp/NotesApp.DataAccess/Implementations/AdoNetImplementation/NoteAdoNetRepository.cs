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
            //sqlConnection.Close();
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
            throw new NotImplementedException();
        }

        public void Add(Note entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Note entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Note entity)
        {
            throw new NotImplementedException();
        }

    }
}
