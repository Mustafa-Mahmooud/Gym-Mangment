using Core.Entites.Identity;
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

namespace Core.CreateToken
{
    public class Token :IToken
    {
        private readonly IConfiguration _Config;

        public Token(IConfiguration configuration)
        {
            _Config = configuration;
        }

        public async Task<string> CreateTokenAsync(AppUser _user, UserManager<AppUser> _userManager)
        {
            // Create claims
            var authClaims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.GivenName, _user.DisplayName),
            new Claim(JwtRegisteredClaimNames.Email, _user.Email)
        };

            // Add roles as claims
            var userRoles = await _userManager.GetRolesAsync(_user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Create signing key
            var authKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_Config["Jwt:Key"] ?? string.Empty)
            );

            // Create JWT token
            var token = new JwtSecurityToken(
                 issuer: _Config["Jwt:Issuer"],
                 audience: _Config["Jwt:Audience"],
                 expires: DateTime.Now.AddHours(double.Parse(_Config["Jwt:ExpiryInHours"] ?? "1")),
                 claims: authClaims,
                 signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
