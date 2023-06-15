using Core.Entities.Abstract;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.AuthDtos
{
    public class AssignedRoleDto : IDto
    {
        public MemberType Role { get; set; }
        public List<Member> HasRole { get; set; }
        public List<Member> HasNotRole { get; set; }
        public string RoleName { get; set; }
        public string[] AddIds { get; set; }
        public string[] DeleteIds { get; set; }
    }
}
