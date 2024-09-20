using AutoMapper;
using DatingApp.Common.Extensions;
using DatingApp.Framework.Business.Interfaces;
using DatingApp.Framework.Business.Models;
using DatingApp.Framework.Business.Models.Response;
using DatingApp.Framework.Data.Context;
using DatingApp.Framework.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DatingApp.API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUsersService _usersService;
        private readonly ILogger<UsersController> _logger;
        private readonly DataContext dataContext;
        private readonly IMapper mapper;

        //protected APIResponse _aPIResponse;


        public UsersController(IUsersService usersService, ILogger<UsersController> logger, DataContext dataContext, IMapper mapper)
        {
            _usersService = usersService ??
                throw new ArgumentNullException(nameof(usersService));
            _logger = logger;
            this.dataContext = dataContext;
            this.mapper = mapper;

            //_aPIResponse = new APIResponse();
        }

        /// <summary>
        /// [FromQuery] to retrive parameters from query string
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetUsers()
        {
            //userParams.CurrentUsername = User.GetUsername();
            var users = await dataContext.Users.Include("Photos").ToListAsync();

            var usersToReturn  = this.mapper.Map<IEnumerable<Member>>(users);

          //  Response.AddPaginationHeader(users);

            return Ok(usersToReturn);
        }

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

        
        /// <summary>
        /// We donot return anything in terms of data to the user in case of Sucessful update.
        /// </summary>
        /// <param name="memberUpdate"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdate memberUpdate)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            //var user = await _usersService.Get(username);

            //if (user == null) return BadRequest("Could not find user");

            //mapper.Map(memberUpdate, user);

            if (await _usersService.UpdateUser(memberUpdate, username)) return NoContent();

            return BadRequest("Failed to update the user");
        }
    }
}
