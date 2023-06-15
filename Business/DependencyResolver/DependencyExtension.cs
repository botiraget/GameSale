using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Business.Mappings.AutoMapper;
using Business.UnitOfWork;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolver
{
    public static class DependencyExtension
    {
        
        public static void AddServiceModule(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IGameService, GameManager>();
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<ICompanyService, CompanyService>();
           

            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new CategoryProfile());
                opt.AddProfile(new GameProfile());
                opt.AddProfile(new CompanyProfile());
                opt.AddProfile(new UserProfile());

            });

            var mapper = configuration.CreateMapper();

            services.AddSingleton(mapper);


        }
    }
}
