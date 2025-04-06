using Core.Application.DTOs;
using Core.Application.Interfaces;
using Infrastructure.Identity.Interfaces;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        public async Task<GenericResponse<AuthResponse?>> LoginAsync(LoginRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new (false, "User not found.", null);
            }

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new (false, "Incorrect email or password.", null);
            }

            var token = await _tokenService.CreateJwtAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            return new (
                true, 
                "Authentication Successful.", 
                new AuthResponse { 
                    UserId= user.Id, 
                    Email = user.Email, 
                    Roles = roles.ToList(), 
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    TokenExpiresOn = token.ValidTo 
                } 
            );
        }
    }
}
