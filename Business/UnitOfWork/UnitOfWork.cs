using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        GameSaleDbContext _context;
        public UnitOfWork()
        {
            _context = new GameSaleDbContext();
        }
        public EfBaseRepository<GameSaleDbContext, Game> GameRepo => new EfBaseRepository<GameSaleDbContext, Game>(_context);

        public EfBaseRepository<GameSaleDbContext, Game_Category> Game_CategoryRepo => new EfBaseRepository<GameSaleDbContext, Game_Category>(_context);

        public EfBaseRepository<GameSaleDbContext, Category> Category => new EfBaseRepository<GameSaleDbContext, Category>(_context);
    }
}

