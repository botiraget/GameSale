using AutoMapper;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Entities.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.Models;
using Entities.Dtos.GameDtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class GameManager : IGameService
    {
        private readonly IGameDal _gameDal;
        private readonly IGame_CategoryDal _gameCategoryDal;
        private readonly IMapper _mapper;
        private readonly GameSaleDbContext _gameSaleDbContext;

        public GameManager(IGameDal gameDal, IMapper mapper, GameSaleDbContext gameSaleDbContext, IGame_CategoryDal gameCategoryDal)
        {
            _gameDal = gameDal;
            _mapper = mapper;
            _gameSaleDbContext = gameSaleDbContext;
            _gameCategoryDal = gameCategoryDal;
        }

        public async Task<bool> AddGame(Game game)
        {
            try
            {
                GameValidator validations = new GameValidator();
                var validationResult = validations.Validate(game);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                if (await GetGameByName(game.GameName) == null)
                {
                    await _gameDal.Add(_mapper.Map<Game>(game));

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task DeleteGame(int id)
        {
            var removedEntity = await _gameDal.GetWithAsNoTracking(id);
            if (removedEntity != null)
            {
                _gameDal.Delete(removedEntity);

            }
            else
                throw new NotImplementedException("hata");
        }

        public async Task<GameDto> GetGameById(int id)
        {
            var game = _mapper.Map<GameDto>(await _gameDal.Get(x => x.GameId == id));
            return game;
        }

        public async Task<GameDto> GetGameByName(string name)
        {

            var game = _mapper.Map<GameDto>(await _gameDal.Get(x => x.GameName.Contains(name)));

            return game;
        }

        public async Task<IList<GameListDto>> GetGameList()
        {
            var gameList = _mapper.Map<List<GameListDto>>(await _gameDal.GetList());
            return gameList;
        }

        //public async Task<IQueryable<GameVmDto>> GetQueryable(int id)
        //{
        //    GameVmDto game_vm_dto = new GameVmDto();

        //    game_vm_dto = await _gameDal.GetQuery(x => x.GameId == id).Result.SingleOrDefaultAsync();


        //    //GameVmDto game_vm_dto = new GameVmDto();

        //    //game_vm_dto.Game = await _gameDal.GetQuery(x => x.GameId == id).Result.SingleOrDefaultAsync();

        //    //Category category = new Category();

        //    //List<Category> categories = new List<Category>();
        //    //foreach (Game_Category item in _gameCategoryDal.GetQuery(x => x.GameId == id).Result.Include("Category").ToList())
        //    //{
        //    //    categories.Add(new Category() { CategoryId = item.CategoryId, CategoryName = item.Category.CategoryName });
        //    //}
        //    //game_vm_dto.Categories= categories;

        //    //return (IQueryable<GameVmDto>)game_vm_dto.Categories;
        //}

        public async Task UpdateGame(Game game)
        {
            GameValidator validations = new GameValidator();
            var validationResult = validations.Validate(game);

            if (validationResult.IsValid)
            {
                var updatedEntity = await _gameDal.GetWithAsNoTracking(game.GameId);
                if (updatedEntity != null)
                {
                    await _gameDal.Update(game);
                    await _gameSaleDbContext.SaveChangesAsync();

                }

            }
            else
                throw new NotImplementedException();
        }
    }
}
