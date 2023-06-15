using Core.DataAccess.Abstract;
using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public static class EfGameSaleExtension
    {

        public static void AddDalModule(this IServiceCollection services)
        {
            services.AddScoped<ICompanyDal, EfCompanyDal>();
            services.AddScoped<ICategoryDal,EfCategoryDal>();
            services.AddScoped<IGameDal, EfGameDal>();
            services.AddScoped<IGame_CategoryDal, EfGame_CategoryDal>();
        }

        
       
    }
}
