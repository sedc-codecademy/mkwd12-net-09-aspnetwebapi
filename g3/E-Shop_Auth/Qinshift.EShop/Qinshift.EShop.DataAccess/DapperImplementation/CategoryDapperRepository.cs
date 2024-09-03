using Dapper;
using Microsoft.Data.SqlClient;
using Qinshift.EShop.DataAccess.Interface;
using Qinshift.EShop.DomainModels;
using static Dapper.SqlMapper;

namespace Qinshift.EShop.DataAccess.DapperImplementation
{
	public class CategoryDapperRepository : IRepository<Category>
	{
		private readonly string _connString;
        public CategoryDapperRepository(string connString)
        {
            _connString = connString;
        }



		public IEnumerable<Category> GetAll()
		{
			using (SqlConnection connection = new SqlConnection(_connString))
			{
				connection.Open();
				List<Category> categories = connection.Query<Category>("SELECT * FROM dbo.Categories").ToList();
				return categories;
			}
		}

		public Category GetById(int id)
		{
			using (SqlConnection connection = new SqlConnection(_connString))
			{
				connection.Open();
				Category category = connection.QuerySingleOrDefault<Category>("SELECT * FROM dbo.Categories WHERE Id = @Id", new { Id = id });
				//Category category = connection.Query<Category>("SELECT * FROM dbo.Categories WHERE Id = @Id", new { Id = id }).SingleOrDefault();
				return category;
			}
		}
        public int Add(Category entity)
		{
			using (SqlConnection connection = new SqlConnection(_connString))
			{
				connection.Open();

				string query = "INSERT INTO dbo.Categories (Name, Description, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy) VALUES (@name, @description, @createdOn, @createdBy, @modifiedOn, @modifiedBy)";

				connection.Query(query, new
				{
					name = entity.Name,
					description = entity.Description,
					createdOn = entity.CreatedOn,
					createdBy = entity.CreatedBy,
					modifiedOn = entity.ModifiedOn,
					modifiedBy = entity.ModifiedBy,
				});
			}

			return 1;
		}
		public int Update(Category entity)
		{
			using (SqlConnection connection = new SqlConnection(_connString))
			{
				connection.Open();

				string query = "UPDATE dbo.Categories SET Name = @name, Description = @description WHERE Id = @id";

				connection.Query(query, new
				{
					id = entity.Id,
					name = entity.Name,
					description = entity.Description,
				});
			}
			return 1;
		}
		public int Remove(int id)
		{
			using (SqlConnection connection = new SqlConnection(_connString))
			{
				connection.Open();

				string query = "DELETE * FROM dbo.Categories WHERE Id = @id";

				connection.Query(query, new
				{
					id = id,
				});
			}
			return 1;
		}
	}
}
