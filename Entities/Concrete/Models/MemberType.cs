using Core.Entities.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Models
{
    public class MemberType : IdentityRole, IEntity
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public MemberType()
        {

        }

        public MemberType(string memberType) :base(memberType)
        {
                
        }
    }
}
