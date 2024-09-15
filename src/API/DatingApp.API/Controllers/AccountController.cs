using AutoMapper;
using DatingApp.Framework.Business.Interfaces;
using DatingApp.Framework.Business.Models;
using DatingApp.Framework.Business.Models.Request;
using DatingApp.Framework.Business.Models.Response;
using DatingApp.Framework.Business.Services;
using DatingApp.Framework.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{

    public class AccountController(UserManager<ApplicationUser> userManager, ITokenService tokenService,
                   IMapper mapper) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterationRequest registerationRequest)
        {
            if (await UserExists(registerationRequest.Username)) return BadRequest("Username is taken");

            var user = mapper.Map<ApplicationUser>(registerationRequest);

            user.UserName = registerationRequest.Username.ToLower();
            var result = await userManager.CreateAsync(user, registerationRequest.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return new User
            {
                Username = user.UserName,
                Token = await tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
        }



        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await userManager.Users
             .Include(p => p.Photos)
                 .FirstOrDefaultAsync(x =>
                     x.NormalizedUserName == loginRequest.UserName.ToUpper());

            if (user == null || user.UserName == null) return Unauthorized("Invalid username");

            return new User
            {
                Username = user.UserName,
                KnownAs = user.KnownAs,
                Token = await tokenService.CreateToken(user),
                Gender = user.Gender,
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
            };

        }

        private async Task<bool> UserExists(string username)
        {
            return await userManager.Users.AnyAsync(x => x.NormalizedUserName == username.ToUpper()); // Bob != bob
        }
    }
}
