using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Business.UnitOfWork;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.Models;
using Entities.Dtos.AuthDtos;
using Entities.Dtos.GameDtos;
using GameSale.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GameSale.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GameSaleDbContext _gameSaleDbContext;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(IAuthService authService, IHttpContextAccessor httpContextAccessor, GameSaleDbContext gameSaleDbContext, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _gameSaleDbContext = gameSaleDbContext;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> ProfileIndex()
        {
            try
            {
                var requestUserName = _unitOfWork.GetUserNameFromJWT(HttpContext);

                var profileId = _gameSaleDbContext.Members.Include(x => x.PurchasedGames).ThenInclude(x => x.Game).Where(x => x.UserName == requestUserName).Select(x => new ProfileDto(x)).FirstOrDefault();


                if (profileId == null)
                {
                    throw new Exception();
                }

                return View(profileId);
            }
            catch (Exception ex)
            {

                return RedirectToAction("Login");
            }



        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var requestUserName = _unitOfWork.GetUserNameFromJWT(HttpContext);


            var profileId = _gameSaleDbContext.Members.Where(x => x.UserName == requestUserName).Select(x => x.Id).FirstOrDefault();

            var profileDto = _mapper.Map<Member>(await _authService.GetProfileById(profileId));


            //
            return View(profileDto);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(ProfileDto model)
        {

            await _authService.Edit(model);

            return RedirectToAction("ProfileIndex");


        }

        public IActionResult Test()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Register(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }


        public IActionResult Login(string returnUrl)
        {
            return View(new LoginDto { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Login(model);

                if (result != null)
                {
                    var cookieOptions = new CookieOptions();
                    cookieOptions.Expires = result.ExpirationDate;
                    Response.Cookies.Append("Auth_JWT", result.Token, cookieOptions);
                    return Redirect(model.ReturnUrl ?? "/");
                }
                ModelState.AddModelError("", "Böyle bir kullanıcı adı bulunmamaktadır.");
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _authService.LogOut();

            Response.Cookies.Delete("Auth_JWT");

            return RedirectToAction("Login");
        }


    }
}
