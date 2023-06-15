using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.GameDtos
{
    public class PurchasedGameDto
    {
        public PurchasedGameDto()
        {


        }

        public List<Game>? Game { get; set; }
        public Member? Member { get; set; }
    }
}
