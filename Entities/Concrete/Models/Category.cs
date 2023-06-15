using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Models
{
    public class Category : IEntity
    {

        public Category()
        {

        }
        public Category(Game_Category x)
        {
            CategoryId = x.CategoryId;
            CategoryName = x.Category.CategoryName;
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }


        public ICollection<Game_Category>? Game_Categories { get; set; }

    }
}
