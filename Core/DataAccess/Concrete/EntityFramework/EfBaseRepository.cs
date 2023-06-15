using Core.DataAccess.Abstract;
using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Concrete.EntityFramework
{
    public class EfBaseRepository<TContext, TEntity> : IBaseRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public EfBaseRepository(TContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task Add(TEntity Model)
        {
            await _context.Set<TEntity>().AddAsync(Model);
            await _context.SaveChangesAsync();
        }

        public void Delete(TEntity Model)
        {
            _context.Set<TEntity>().Remove(Model);
            _context.SaveChangesAsync();

        }

        public async Task<TEntity> Find(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IList<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? await _context.Set<TEntity>().ToListAsync() : await _context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> expression = null)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        public async Task Update(TEntity Model)
        {
            _context.Set<TEntity>().Update(Model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? await _dbSet.ToListAsync() : await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<IQueryable<TEntity>> GetQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            return  _dbSet.Where(predicate);

        }
    }
}
