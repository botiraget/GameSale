using Entities.Concrete.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class GameSaleDbContext : IdentityDbContext<Member, MemberType, string>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=BLNTTYUKSEL\\SQLEXPRESS;Database=GameSale_DB;User Id = sa; Password=123456;encrypt=false;MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
        }

        public GameSaleDbContext(DbContextOptions<GameSaleDbContext> options) : base(options)
        {

        }
        public GameSaleDbContext()
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Game_Category> Game_Categories { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberType> MemberTypes { get; set; }
        public DbSet<PurchasedGame> PurchasedGames { get; set; }



    }
}
