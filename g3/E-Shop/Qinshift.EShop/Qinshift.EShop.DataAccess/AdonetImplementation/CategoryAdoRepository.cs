using Microsoft.Data.SqlClient;
using Qinshift.EShop.DataAccess.Interface;
using Qinshift.EShop.DomainModels;

namespace Qinshift.EShop.DataAccess.AdonetImplementation
{
	public class CategoryAdoRepository : IRepository<Category>
	{
		private readonly string _connString;

        public CategoryAdoRepository(string connString)
        {
            _connString = connString;
        }


        public IEnumerable<Category> GetAll()
		{
			// 1. Create new connection to SQL db
			SqlConnection connection = new SqlConnection( _connString);

			// 2. Open the connection
			connection.Open();

			// 3. Create sql command
			SqlCommand cmd = new SqlCommand();
			cmd.Connection = connection;
			cmd.CommandText = "SELECT * FROM dbo.Categories";

			List<Category> categories = new List<Category>();

			SqlDataReader reader = cmd.ExecuteReader();

			while(reader.Read())
			{
				// First approach of getting values from db
				//categories.Add(new Category
				//{
				//	Id = reader.GetInt32(0),
				//	Name = reader.GetString(1),
				//	Description = reader.GetString(2)
				//});

				// Second approach of getting values from db
				//categories.Add(new Category()
				//{
				//	Id = reader.GetFieldValue<int>(0),
				//	Name = reader.GetFieldValue<string>(1),
				//	Description = reader.GetFieldValue<string>(2),
				//	CreatedOn = reader.GetFieldValue<DateTime>(3),
				//});

				categories.Add(new Category()
				{
					Id = (int)reader["Id"],
					Name = (string)reader["Name"],
					Description = (string)reader["Description"],
					CreatedOn = (DateTime)reader["CreatedOn"]
				});
			}

			connection.Close();

			return categories;
		}
		public Category GetById(int id)
		{
			SqlConnection connection = new SqlConnection(_connString);
			connection.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = connection;
			// bad appropach. Use method to add paramteres
			//cmd.CommandText = $"SELECT * FROM dbo.Categories where Id = {id}";
			cmd.CommandText = "SELECT * FROM dbo.Categories where Id = @Id";
			cmd.Parameters.AddWithValue("@Id", id);


			List<Category> categories = new List<Category>();
			SqlDataReader reader = cmd.ExecuteReader();

			while(reader.Read())
			{
				categories.Add(new Category()
				{
					Id = (int)reader["Id"],
					Name = (string)reader["Name"],
					Description = (string)reader["Description"],
					CreatedOn = (DateTime)reader["CreatedOn"]
				});
			}
			connection.Close();

			return categories.FirstOrDefault();
		}

		public int Add(Category entity)
		{
			SqlConnection connection = new SqlConnection(_connString);
			connection.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = connection;
			cmd.CommandText = "INSERT INTO dbo.Categories(Text, Description) " +
							  "VALUES(@name, @description)";

			cmd.Parameters.AddWithValue("@name", entity.Name);
			cmd.Parameters.AddWithValue("@description", entity.Description);

			int rowsAffected = cmd.ExecuteNonQuery();

			connection.Close();
			return rowsAffected;
		}
		public int Update(Category entity)
		{
			throw new NotImplementedException();
		}
		public int Remove(int id)
		{
			throw new NotImplementedException();
		}
	}
}
