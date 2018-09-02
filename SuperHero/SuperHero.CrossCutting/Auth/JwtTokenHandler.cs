using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SuperHero.Domain.Auth;
using SuperHero.Domain.Models;

namespace SuperHero.CrossCutting.Auth
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly string _jwtUrl;
        private readonly string _secretKey;

        public JwtTokenHandler(IConfiguration configuration)
        {
            _jwtUrl = configuration.GetSection("AppConfig").GetSection("JwtUrl").Value;
            _secretKey = configuration.GetSection("AppConfig").GetSection("SecretKey").Value;
        }

        public string Generate(UserModel user)
        {
            List<Claim> userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var claim in user.Roles)
                userClaims.Add(new Claim(ClaimTypes.Role, claim.Name));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_jwtUrl,
                                             _jwtUrl,
                                             userClaims,
                                             expires: DateTime.Now.AddDays(30),
                                             signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
