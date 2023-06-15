using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.CompanyDtos
{
    public class CompanyDto : IDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
