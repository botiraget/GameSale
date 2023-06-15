using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Models
{
    public class Company : IEntity
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }


        public ICollection<Game>? Games { get; set; }

    }
}
