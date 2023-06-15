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
    public class EfCompanyDal : EfBaseRepository<GameSaleDbContext, Company>,ICompanyDal
    {
        private readonly GameSaleDbContext _context;
        public EfCompanyDal(GameSaleDbContext context) : base(context)
        {
            _context = context;  
        }

        public async Task<Company> GetWithAsNoTracking(int id)
        {
            return await _context.Companies.AsNoTracking().FirstOrDefaultAsync(x => x.CompanyId == id);
        }
    }
}
