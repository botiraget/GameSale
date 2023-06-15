using AutoMapper;
using Entities.Concrete.Models;
using Entities.Dtos.CategoryDtos;
using Entities.Dtos.GameDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mappings.AutoMapper
{
    public class GameProfile: Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GameListDto>().ReverseMap();
            CreateMap<Game, GameDto>().ReverseMap();
            CreateMap<Game, GameVmDto>().ReverseMap();
            CreateMap<PurchasedGame, PurchasedGameDto > ().ReverseMap();

        }
    }
}
