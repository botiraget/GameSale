using Core.Entities.Abstract;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entities.Dtos.AuthDtos
{
    public class ProfileDto : IDto
    {
        private Member x;

        public ProfileDto()
        {

        }
        public ProfileDto(Member x)
        {
            Games = x.PurchasedGames.Select(x => new Game(x))?.ToList();
            Member = x;
          
            //Categories = x.Game_Categories.Select(x => new Category(x))?.ToList();
            //Comments = x.Comments?.ToList();

        }

        public Guid id { get; set; }
        public string UserName { get; set; }
        public string? Avatar { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public Member ?Member { get; set; }
        public List<Game>? Games { get; set; }

    }
}
