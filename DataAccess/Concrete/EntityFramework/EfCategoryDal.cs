using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfBaseRepository<GameSaleDbContext, Category>, ICategoryDal
    {
        private readonly GameSaleDbContext _context;
        public EfCategoryDal(GameSaleDbContext context) : base(context)
        {
            _context = context;
        }
    

        public async Task<Category> GetWithAsNoTracking(int id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.CategoryId == id);
        }
    }
}
