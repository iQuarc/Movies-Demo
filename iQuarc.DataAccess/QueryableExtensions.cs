using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iQuarc.DataAccess
{
	public static class QueryableExtensions
	{
		public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> query)
		{
			return await System.Data.Entity.QueryableExtensions.ToListAsync(query);
		}

		public static async Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> query)
		{
			return await System.Data.Entity.QueryableExtensions.FirstOrDefaultAsync(query);
		}
	}
}