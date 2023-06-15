using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Models
{
    public class PurchasedGame : IEntity
    {
        public int PurchasedGameId { get; set; }
        public int GameId { get; set; }
        public string MemberId { get; set; }

        public Game? Game { get; set; }
        public Member? Member { get; set; }
    }
}
