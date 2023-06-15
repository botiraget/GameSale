using AutoMapper;
using Business.Abstract;
using Entities.Concrete.Models;
using Entities.Dtos.AuthDtos;
using Entities.Dtos.CategoryDtos;
using Entities.Dtos.GameDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<Member> _userManager;
        private readonly SignInManager<Member> _signInManager;
        private readonly IPasswordHasher<Member> _passwordHasher;
        private readonly RoleManager<MemberType> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AuthManager(UserManager<Member> userManager, SignInManager<Member> signInManager, IPasswordHasher<Member> passwordHasher, RoleManager<MemberType> roleManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<IdentityResult> Edit(ProfileDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

           
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Avatar = model.Avatar;
          

            var result = await _userManager.UpdateAsync(user);

            return result;
        }

        public async Task<ProfileDto> GetProfileById(string id)
        {
            var profile = _mapper.Map<ProfileDto>(await _userManager.FindByIdAsync(id));
            return profile;
        }

        public async Task<LoginResponseDto> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);
                var sigInResult =  await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);

                if (!sigInResult.Succeeded)
                {
                    return null;
                }

                return new LoginResponseDto()
                {
                    ExpirationDate = token.ValidTo,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                };
            }
            return null;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterDto model)
        {
            Member appIdentityUser = new Member { UserName = model.UserName, Email = model.Email };

            var result = await _userManager.CreateAsync(appIdentityUser, model.Password);
            if (!result.Succeeded)
                return result;

            if (!await _roleManager.RoleExistsAsync(MemberType.Admin))
            {
                await _roleManager.CreateAsync(new MemberType(MemberType.Admin));
                await _userManager.AddToRoleAsync(appIdentityUser, MemberType.Admin);
            }

            if (await _roleManager.RoleExistsAsync(MemberType.Admin))
            {
                if (!await _roleManager.RoleExistsAsync(MemberType.User))
                {
                    await _roleManager.CreateAsync(new MemberType(MemberType.User));

                }
                if (await _roleManager.RoleExistsAsync(MemberType.User))
                    await _userManager.AddToRoleAsync(appIdentityUser, MemberType.User);
            }

              
             

            return result;
        }


        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }


}
