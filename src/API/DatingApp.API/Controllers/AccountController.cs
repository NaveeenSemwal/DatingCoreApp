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
        public async Task<ActionResult<Member>> Register([FromBody] RegisterationRequest registerationRequest)
        {
            //var registerationResponse = await _usersService.Register(registerationRequest);

            //if (registerationResponse.ErrorMessages.Count == 0)
            //{
            //    _aPIResponse.IsSuccess = true;
            //    _aPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
            //}
            //else
            //{
            //    _aPIResponse.IsSuccess = false;
            //    _aPIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            //    _aPIResponse.ErrorMessages = registerationResponse.ErrorMessages;
            //}

            //_aPIResponse.Data = registerationResponse;

            return Ok();
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
    }
}
