using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Models
{
    public class Game : IEntity
    {
        public Game()
        {

        }
        public Game(PurchasedGame x)
        {
            GameId= x.Game.GameId;
            GameName = x.Game.GameName;
            CompanyId = x.Game.CompanyId;
            Price = x.Game.Price;
            GameCoverPhoto = x.Game.GameCoverPhoto;
            DownloadLink = x.Game.DownloadLink;
            SystemRequirements = x.Game.SystemRequirements;
        }

        public int GameId { get; set; }
        public string GameName { get; set; }
        public int CompanyId { get; set; }
        public decimal Price { get; set; }
        public string GameCoverPhoto { get; set; }
        public string DownloadLink { get; set; }
        public string SystemRequirements { get; set; }
       


        public ICollection<Game_Category>? Game_Categories { get; set; }
        public ICollection<PurchasedGame>? PurchasedGames { get; set; }
        public Company? Company { get; set; }









    }
}
