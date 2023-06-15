using AutoMapper;
using Entities.Concrete.Models;
using Entities.Dtos.CategoryDtos;
using Entities.Dtos.CompanyDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mappings.AutoMapper
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Company, CompanyListDto>().ReverseMap();
        }
    }
}
