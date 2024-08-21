using Microsoft.EntityFrameworkCore;
using Qinshift.EShop.DataAccess.Interface;
using Qinshift.EShop.DomainModels;

namespace Qinshift.EShop.DataAccess.Implementation
{
	public class DataRepository<T> : IRepository<T> where T : BaseEntity
	{
		private readonly EShopDbContext _context;
		private DbSet<T> table;

		public DataRepository(EShopDbContext context)
		{
			_context = context;
			table = _context.Set<T>();
        }

		public IEnumerable<T> GetAll()
		{
			return table.ToList();
		}
		public T GetById(int id)
		{
			return table.SingleOrDefault(x => x.Id == id);
		}

		public int Add(T entity)
		{
			table.Add(entity);
			return _context.SaveChanges();
		}

		public int Update(T entity)
		{
			table.Update(entity);
			return _context.SaveChanges();
		}

		public int Remove(int id)
		{
			var entity = table.SingleOrDefault(x => x.Id == id);
			table.Remove(entity);
			return _context.SaveChanges();
		}
	}
}
