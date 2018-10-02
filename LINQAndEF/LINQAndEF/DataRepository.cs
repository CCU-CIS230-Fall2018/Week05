using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LINQAndEF
{
	/// <summary>
	/// Represents a repository for data of a given type.
	/// </summary>
	/// <typeparam name="T">
	/// The type of data to manage with the repository.
	/// </typeparam>
	public class DataRepository<TEntity> : IDisposable where TEntity : class
	{
		NorthwindDatabaseModel context;
		DbSet<TEntity> set;

		/// <summary>
		/// Initializes a new instance of the <see cref="DataRepository"/> class.
		/// </summary>
		public DataRepository()
		{
			context = new NorthwindDatabaseModel();
			set = context.Set<TEntity>();
		}

		/// <summary>
		/// Disposes of managed and unmanaged resources used by the object.
		/// </summary>
		public void Dispose()
		{
			if (context != null)
			{
				context.Dispose();
				context = null;
			}
		}

		/// <summary>
		/// Filters a sequence of entities based on the specified predicate.
		/// </summary>
		/// <param name="predicate">
		/// A function to test each element for a condition.
		/// </param>
		/// <returns>
		/// An IQueryable<T> that contains elements from the input sequence that satisfy the condition 
		/// specified by predicate.
		/// </returns>
		public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
		{
			return set.Where(predicate);
		}

		/// <summary>
		/// Adds the specified entity.
		/// </summary>
		/// <param name="entity">
		/// The entity to add.
		/// </param>
		/// <returns>
		/// The added entity.
		/// </returns>
		public TEntity Add(TEntity entity)
		{
			return set.Add(entity);
		}

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">
		/// The entity to delete.
		/// </param>
		public void Delete(TEntity entity)
		{
			set.Remove(entity);
		}

		/// <summary>
		/// Saves all changes to the underlying data store.
		/// </summary>
		public void Save()
		{
			context.SaveChanges();
		}
	}
}
