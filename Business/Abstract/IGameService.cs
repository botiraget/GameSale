using Entities.Concrete.Models;
using Entities.Dtos.GameDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGameService
    {
        Task<bool> AddGame(Game game);
        Task UpdateGame(Game game);
        Task DeleteGame(int id);
        Task<GameDto> GetGameById(int id);
        Task<GameDto> GetGameByName(string name);
        Task<IList<GameListDto>> GetGameList();

       //Task<IQueryable<GameVmDto>>GetQueryable(int id);
    }
}
