using DatingApp.Common.Helpers;
using DatingApp.Framework.Business.Interfaces;
using DatingApp.Framework.Business.Models.Response;
using DatingApp.Framework.Data.Context;
using DatingApp.Framework.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUsersService _usersService;
        private readonly ILogger<UsersController> _logger;
        //protected APIResponse _aPIResponse;


        public UsersController(IUsersService usersService, ILogger<UsersController> logger)
        {
            _usersService = usersService ??
                throw new ArgumentNullException(nameof(usersService));
            _logger = logger;

            //_aPIResponse = new APIResponse();
        }

        /// <summary>
        /// [FromQuery] to retrive parameters from query string
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>

        //[HttpGet]
        //public async Task<PagedList<Member>> GetAll([FromQuery] UserParams searchParams)
        //{
        //    return await _usersService.GetAll(searchParams);
        //}

        /// <summary>
        /// The Route data by default is string. So no need of {username : string}
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("{username}")]
        public async Task<Member> GetbyUserName(string username)
        {
            return await _usersService.Get(username);
        }
    }
}
