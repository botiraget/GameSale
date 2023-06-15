using Entities.Concrete.Models;
using Entities.Dtos.AuthDtos;
using Entities.Dtos.CategoryDtos;
using Entities.Dtos.GameDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        //Task<List<AssignedRoleDto>> GetUserList();

        Task<IdentityResult> Register(RegisterDto model);
        Task<LoginResponseDto> Login(LoginDto model);
        Task<IdentityResult> Edit(ProfileDto model);
        Task LogOut();
        Task<ProfileDto> GetProfileById(string id);
    }
}
