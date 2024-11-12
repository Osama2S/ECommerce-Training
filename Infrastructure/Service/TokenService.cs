using Core.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _symmetricSecurityKey;
        private readonly IConfiguration _configuration;
        public TokenService( IConfiguration configuration)
        {
            _configuration=configuration;
            var key = _configuration["Token:Key"];
            if (string.IsNullOrEmpty(key) || key.Length < 32)
            {
                throw new InvalidOperationException("The Token:Key in configuration must be at least 32 characters long for HMAC SHA-256.");
            }
            _symmetricSecurityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                new Claim(JwtRegisteredClaimNames.Name,user.DisplayName)
            };
            var cred = new SigningCredentials(_symmetricSecurityKey,SecurityAlgorithms.HmacSha512Signature);

            var tokenDiscreptor = new SecurityTokenDescriptor
            {
                 Subject=new ClaimsIdentity(claims),
                 Expires=DateTime.Now.AddDays(7),
                 SigningCredentials=cred,
                Issuer=_configuration["Token:Issuer"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDiscreptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
