using AutoMapper;
using Entities.Concrete.Models;
using Entities.Dtos.CategoryDtos;
using Entities.Dtos.GameDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mappings.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryListDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, GameVmDto>().ReverseMap();
          
        }
    }
}
