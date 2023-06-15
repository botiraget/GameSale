using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.UnitOfWork
{
    public interface IUnitOfWork
    {
        public EfBaseRepository<GameSaleDbContext,Game > GameRepo { get; }

        public EfBaseRepository<GameSaleDbContext, Game_Category> Game_CategoryRepo { get; }

        public EfBaseRepository<GameSaleDbContext, Category> Category { get; }


        public string GetUserNameFromJWT(HttpContext httpContext)
        {
            var user = httpContext.Request.Cookies["Auth_JWT"];

            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtModel = jwtHandler.ReadJwtToken(user);

            string requestUserName = jwtModel.Claims.Where(x => x.Type == ClaimTypes.Name).Select(y => y.Value).FirstOrDefault();

            return requestUserName;
        }
    }
}
