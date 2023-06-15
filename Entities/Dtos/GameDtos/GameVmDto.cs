using Core.Entities.Abstract;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.GameDtos
{
    public class GameVmDto : IDto
    {

        public GameVmDto()
        {

        }
        public GameVmDto(Game x)
        {   
            Game = x;
            Categories = x.Game_Categories.Select(x => new Category(x))?.ToList();
          
        }

        public Game Game { get; set; }
        public List<Category> Categories { get; set; }
        public Category Category { get; set; }




      
    }
}
