using Core.Entities.Abstract;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.GameDtos
{
    public class GameDto : IDto
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public string CompanyName { get; set; }
        public decimal Price { get; set; }
        public string ?GameCoverPhoto { get; set; }
        public string ?DownloadLink { get; set; }
        public string ?SystemRequirements { get; set; }


    }
}
