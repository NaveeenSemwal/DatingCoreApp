using DatingApp.Framework.Business.Models.Request;
using DatingApp.Framework.Business.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
 
    public class AccountController : BaseApiController
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
        public async Task<ActionResult<Member>> Login([FromBody] LoginRequest loginRequest)
        {
            //var loginResponse = await _usersService.Login(loginRequest);

            //if (loginResponse == null || loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            //{
            //    _aPIResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            //    _aPIResponse.IsSuccess = false;
            //    _aPIResponse.ErrorMessages.Add("Username and Password is incorrect.");

            //    return BadRequest(_aPIResponse);
            //}

            //_aPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
            //_aPIResponse.IsSuccess = true;
            //_aPIResponse.Data = loginResponse;

            //return Ok(_aPIResponse);
            return Ok();
        }
    }
}
