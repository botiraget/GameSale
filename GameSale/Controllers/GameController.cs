using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Business.UnitOfWork;
using Core.DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.Models;
using Entities.Dtos.AuthDtos;
using Entities.Dtos.GameDtos;
using GameSale.Middleware;
using GameSale.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace GameSale.Controllers
{
    [MyAuthorizationAttribute(MemberType.User)]
    public class GameController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        private readonly GameSaleDbContext _gameSaleDbContext;


        public GameController(IGameService gameService, IMapper mapper, IUnitOfWork unitOfWork, GameSaleDbContext gameSaleDbContext)
        {
            _gameService = gameService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _gameSaleDbContext = gameSaleDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var games = await _gameService.GetGameList();
            if (games == null)
            {
                throw new Exception();
            }
            return View(games);
        }

        public async Task<IActionResult> Details(int id)
        {


            var game_vm = _gameSaleDbContext.Games.Include(x => x.Company).Include(x => x.Game_Categories).ThenInclude(x => x.Category).Where(x => x.GameId == id).Select(x=> new GameVmDto(x)).FirstOrDefault();


            return View(game_vm);
        }

        public IActionResult Create()
        {
            GameVmDto gameVmDto= new GameVmDto();

          

            return View(gameVmDto);
        }

        [HttpPost]

        public async Task<IActionResult> Create(Game game, int id)
        {
            if (ModelState.IsValid)
            {
                 await _gameService.AddGame(game);

                await _gameSaleDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }



            return View(game);
        }
        
        [HttpGet] 
        public async Task<IActionResult> Edit(int id)
        {

            var response = _mapper.Map<Game>(await _gameService.GetGameById(id));
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Game response)
        {

            await _gameService.UpdateGame(response);

            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var response = _mapper.Map<Game>(await _gameService.GetGameById(id));
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, string ex)
        {

            await _gameService.DeleteGame(id);

            return RedirectToAction("Index");


        }



        public async Task<IActionResult> Purchase(int id)
        {
            var game = _mapper.Map<GameDto>(await _gameService.GetGameById(id));
            if (game == null)
            {
                throw new Exception();
            }


            return View(game);

        }

        [HttpPost]
        public async Task<IActionResult> Purchase(int id, string ex)
        {
           
            var requestUserName = _unitOfWork.GetUserNameFromJWT(HttpContext);
          


            var profileId = _gameSaleDbContext.Members.Where(x => x.UserName == requestUserName).Select(x => x.Id).FirstOrDefault();


            var purchasedGame =  await _gameSaleDbContext.Members.Include(x => x.PurchasedGames).ThenInclude(x => x.Game).Where(x => x.Id == profileId).Select(x => new MemberPurchasedGameDto(x)).SingleOrDefaultAsync();



            for (int i = 0; i < purchasedGame.Game.Count; i++)
            {
                if (purchasedGame.Game[i].GameId == id)
                {
                    return RedirectToAction("ProfileIndex", "Account");

                }


            }
           

            await _gameSaleDbContext.AddAsync(new PurchasedGame { GameId = id, MemberId = profileId });

            await _gameSaleDbContext.SaveChangesAsync();


            return RedirectToAction("ProfileIndex", "Account");

        }

 

    }
}

