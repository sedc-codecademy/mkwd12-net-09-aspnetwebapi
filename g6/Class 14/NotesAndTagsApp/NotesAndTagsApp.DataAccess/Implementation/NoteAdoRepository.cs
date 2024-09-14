using Microsoft.Data.SqlClient;
using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTAgsApp.Domain.Enums;
using NotesAndTAgsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.DataAccess.Implementation
{
    public class NoteAdoRepository : IRepository<Note>
    {
        private string _connectionString;

        public NoteAdoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Note entity)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;

            //bad approach
            //command.CommandText = "INSERT INTO dbo.Notes(Text, Priority, Tag, UserId)" +
            //    "VALUES(" + entity.Text 

            command.CommandText = "INSERT INTO dbo.Notes(Text, Priority, Tag, UserId)" +
                                  "VALUES(@text, @priority, @tag, @userId)";

            command.Parameters.AddWithValue("@text", entity.Text);
            command.Parameters.AddWithValue("@priority", entity.Priority);
            command.Parameters.AddWithValue("@tag", entity.Tag);
            command.Parameters.AddWithValue("@userId", entity.UserId);

            command.ExecuteNonQuery(); //returns only rows affected, it does not data that we need to read

            sqlConnection.Close();
        }

        public void Delete(Note entity)
        {
            throw new NotImplementedException();
        }

        public List<Note> GetAll()
        {
            //1. Create new connection to SQL db
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            //2.Open the connection
            sqlConnection.Open();

            //3.Create sql command
            SqlCommand command = new SqlCommand();

            //4.connect the command
            command.Connection = sqlConnection;

            //5.write the command
            command.CommandText = "SELECT * FROM dbo.Notes N INNER JOIN dbo.Users U ON U.Id = N.UserId";

            List<Note> notesDb = new List<Note>();

            //6.Execute the command
            SqlDataReader sqlDataReader = command.ExecuteReader(); //returns table with data that we need to read

            while (sqlDataReader.Read())
            {
                notesDb.Add(new Note()
                {
                    Id = (int)sqlDataReader["Id"],
                    Text = (string)sqlDataReader["Text"],
                    Priority = (PriorityEnum)sqlDataReader["Priority"],
                    Tag = (TagEnum)sqlDataReader["Tag"],
                    UserId = (int)sqlDataReader["UserId"],
                    User = new User()
                    {
                        Firstname = (string)sqlDataReader["Firstname"],
                        Lastname = (string)sqlDataReader["Lastname"]
                    }
                });
            }
            //7.close the connection!!
            sqlConnection.Close();

            return notesDb;
        }

        public Note GetById(int id)
        {
            //1. Create new connection to SQL db
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            //2.Open the connection
            sqlConnection.Open();

            //3.Create sql command
            SqlCommand command = new SqlCommand();

            //4.connect the command
            command.Connection = sqlConnection;

            //5.write the command
            command.CommandText = "SELECT * FROM dbo.Notes N INNER JOIN dbo.Users U ON U.Id = N.UserId WHERE N.Id = @id";
            command.Parameters.AddWithValue("@id", id); //we add the param with the value; the param starts with @

            //6.Execute the command
            SqlDataReader sqlDataReader = command.ExecuteReader();
            List<Note> notesDb = new List<Note>();
            while (sqlDataReader.Read())
            {
                notesDb.Add(new Note()
                {
                    Id = (int)sqlDataReader["Id"],
                    Text = (string)sqlDataReader["Text"],
                    Priority = (PriorityEnum)sqlDataReader["Priority"],
                    Tag = (TagEnum)sqlDataReader["Tag"],
                    UserId = (int)sqlDataReader["UserId"],
                    User = new User()
                    {
                        Firstname = (string)sqlDataReader["Firstname"],
                        Lastname = (string)sqlDataReader["Lastname"]
                    }
                });
            }
            //7.close the connection!!
            sqlConnection.Close();

            return notesDb.FirstOrDefault();
        }

        public void Update(Note entity)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;

            command.CommandText = "UPDATE dbo.Notes SET Text=@text, Tag=@tag, Priority=@priority, UserId=@userId " +
                                   "WHERE Id = @id"; //we must specify the id of the row that we want to update!

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
