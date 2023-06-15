using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Models
{
    public class Game_Category : IEntity
    {

        [Key]
        public int GameCategoryId { get; set; }
        public int GameId { get; set; }
        public int CategoryId { get; set; }


        public Game? Game { get; set; }
        public Category? Category { get; set; }


    }
}
