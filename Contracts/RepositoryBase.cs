using Entities.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DbSet<T> dbSet;
        protected DBContext _context;
        protected readonly ILogger _logger;
        public RepositoryBase(DBContext context, ILogger logger)
        {
            this._context = context;
            this.dbSet = context.Set<T>();
            this._logger = logger;
        }

        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await dbSet.Where(expression).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return dbSet;
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task Create(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual async Task Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual async Task Update(T entity)
        {
            //Implemented in each Repository Children
            throw new NotImplementedException();
        }
    }
}
