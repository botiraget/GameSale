using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Abstract
{
    public interface IBaseRepository<T> where T: IEntity, new()
    {
        Task Add(T Model);
        Task Update(T Model);
        void Delete(T Model);
        Task<T> Get(Expression<Func<T, bool>> expression);
        Task<IList<T>> GetList(Expression<Func<T, bool>> filter = null);
        Task<T> Find(int id);
        Task<List<T>> GetAll(Expression<Func<T, bool>> expression = null);

        Task<IQueryable<T>> GetQueryable(Expression<Func<T, bool>> predicate);


    }
}
