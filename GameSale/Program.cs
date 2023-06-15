
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Entities.Concrete.Models;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Business.DependencyResolver;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GameSale.Middleware;
using Business.UnitOfWork;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();


        var connectionString = builder.Configuration.GetSection("GameSaleDbConnection").Get<string>() ?? throw new InvalidOperationException("Connection string 'GameSaleDbConnection' not found.");

        builder.Services.AddDbContext<GameSaleDbContext>(x => x.UseSqlServer(connectionString));


        builder.Services.AddDalModule();
        builder.Services.AddServiceModule();
        builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
        builder.Services.AddIdentity<Member, MemberType>(x =>
         {
             x.SignIn.RequireConfirmedEmail = false;
             x.SignIn.RequireConfirmedAccount = false;
             x.SignIn.RequireConfirmedPhoneNumber = false;

             x.User.RequireUniqueEmail = true;

             x.Password.RequiredLength = 1;
             x.Password.RequireNonAlphanumeric = false;
             x.Password.RequireUppercase = false;
             x.Password.RequireLowercase = false;

             //x.Lockout.MaxFailedAccessAttempts = 5;
             //x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
         })        
         .AddEntityFrameworkStores<GameSaleDbContext>()
         .AddDefaultTokenProviders();


        //Adding Authentication
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })

        // Adding Jwt Bearer
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWT:ValidAudience"],
                ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
            };

        });


        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.AddAuthorizationMiddleware();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}