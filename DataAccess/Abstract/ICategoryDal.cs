using Core.DataAccess.Abstract;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICategoryDal : IBaseRepository<Category>
    {
        Task<Category> GetWithAsNoTracking(int id);

    }
}
