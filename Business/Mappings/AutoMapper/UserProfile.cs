using AutoMapper;
using Entities.Concrete.Models;
using Entities.Dtos.AuthDtos;
using Entities.Dtos.GameDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mappings.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Member, ProfileDto>().ReverseMap();

            CreateMap<Member, MemberPurchasedGameDto>().ReverseMap();

        }
    }
}
