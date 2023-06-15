using Azure.Core;
using Entities.Concrete.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Net.Http.Headers;

namespace GameSale.Middleware
{
    public class MyAuthorizationAttribute : Attribute
    {
        public string Role { get; set; }

        public MyAuthorizationAttribute(string Role)
        {
            this.Role = Role;

        }
    }
}
