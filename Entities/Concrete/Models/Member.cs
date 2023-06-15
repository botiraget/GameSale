using Core.Entities.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities.Concrete.Models
{
    
    public class Member : IdentityUser, IEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Avatar { get; set; }
        

        public ICollection<PurchasedGame>? PurchasedGames { get; set; }
        public ICollection<MemberType> MemberTypes { get; set; }
    }
}
