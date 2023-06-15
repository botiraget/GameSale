using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.CategoryDtos
{
    public class CategoryListDto : IDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
