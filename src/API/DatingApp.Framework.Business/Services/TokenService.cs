using DatingApp.Framework.Business.Interfaces;
using DatingApp.Framework.Data.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace DatingApp.Framework.Business.Services
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        public async Task<string> CreateToken(Data.Model.ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["TokenKey"]) ?? throw new Exception ("Cannot access token key from appsettings");
            if(key.Length < 64)  throw new Exception("Your token key needs to be longer");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GetClaimsIdentity(user),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }



        private ClaimsIdentity GetClaimsIdentity(ApplicationUser user)
        {
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim("id", user.Id.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Name, user.NormalizedUserName));

            //foreach (var item in roles)
            //{
            //    claims.AddClaim(new Claim(ClaimTypes.Role, item));
            //}

            return new ClaimsIdentity(claims);
        }
    }
}

