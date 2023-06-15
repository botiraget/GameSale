using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfGame_CategoryDal : EfBaseRepository<GameSaleDbContext, Game_Category>, IGame_CategoryDal
    {
        private readonly GameSaleDbContext _context;
        public EfGame_CategoryDal(GameSaleDbContext context) : base(context)
        {
            _context = context;
        }
        public Task<Game_Category> GetWithAsNoTracking(int id)
        {
            throw new NotImplementedException();
        }

    }
}
