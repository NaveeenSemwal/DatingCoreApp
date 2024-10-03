using AutoMapper;
using DatingApp.Common.Extensions;
using DatingApp.Framework.Business.Interfaces;
using DatingApp.Framework.Business.Models;
using DatingApp.Framework.Business.Models.Response;
using DatingApp.Framework.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUsersService _usersService;
        private readonly IPhotoService _photoService;
        private readonly ILogger<UsersController> _logger;
        private readonly DataContext dataContext;
        private readonly IMapper mapper;

        //protected APIResponse _aPIResponse;


        public UsersController(IUsersService usersService, IPhotoService photoService, ILogger<UsersController> logger, DataContext dataContext, IMapper mapper)
        {
            _usersService = usersService ??
                throw new ArgumentNullException(nameof(usersService));
            _photoService = photoService;
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
            if (await _usersService.UpdateUser(memberUpdate, User.GetUsername())) return NoContent();

            return BadRequest("Failed to update the user");
        }

        /// <summary>
        /// For HTTP POST  - we return 201 status : Resource created
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("add-photo")]
        public async Task<ActionResult<MemberPhoto>> AddPhoto(IFormFile file)
        {
            var memeberPhoto = await _photoService.AddPhotoAsync(file, User.GetUsername());

            if (string.IsNullOrWhiteSpace(memeberPhoto.Url)) return BadRequest("Error occured while Adding photo to the user");

            return CreatedAtAction(nameof(GetbyUserName),
                new { username = User.GetUsername() }, memeberPhoto);
        }

        [HttpPut("set-main-photo/{photoId:int}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            if(await _usersService.SetMainPhoto(photoId, User.GetUsername()))
                return NoContent();

            return BadRequest("Problem setting main photo");
        }


        [HttpDelete("delete-photo/{photoId:int}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            if (await _usersService.DeleteUserPhoto(photoId, User.GetUsername())) return Ok();

            return BadRequest("Problem deleting photo");
        }
    }
}
