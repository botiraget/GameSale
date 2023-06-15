using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entities.Dtos.AuthDtos
{
    public class MemberPurchasedGameDto
    {
        public MemberPurchasedGameDto()
        {

        }
        public MemberPurchasedGameDto(Member x)
        {
            Member = x;
            Game = x.PurchasedGames.Select(x => new Game(x))?.ToList();

        }

        public List<Game>? Game { get; set; }
        public Member? Member { get; set; }
    }
}
