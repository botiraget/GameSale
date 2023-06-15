using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfGameDal : EfBaseRepository<GameSaleDbContext, Game>, IGameDal
    {
        private readonly GameSaleDbContext _context;
        public EfGameDal(GameSaleDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Game> GetWithAsNoTracking(int id)
        {
            return await _context.Games.AsNoTracking().FirstOrDefaultAsync(x => x.GameId == id);
        }
    }
}
