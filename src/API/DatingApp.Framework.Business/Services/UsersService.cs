using AutoMapper;
using DatingApp.Framework.Business.Interfaces;
using DatingApp.Framework.Business.Models;
using DatingApp.Framework.Business.Models.Request;
using DatingApp.Framework.Business.Models.Response;
using DatingApp.Framework.Data.Interfaces;
using DatingApp.Framework.Data.Model;
using DatingApp.Framework.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Business.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        /// <summary>
        /// Get the user from HTTpContext
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;
        //private readonly IPhotoService _photoService;

        public UsersService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager, IHttpContextAccessor httpContextAccessor, ITokenService tokenService
           )
        {
            _unitOfWork = unitOfWork;

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
            //_photoService = photoService;
        }

        

        public async Task<Member> Get(string username)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(x => x.UserName == username, false, includeProperties: "Photos");

            return _mapper.Map<Member>(user);
        }

        public async Task<Member> Get(object id)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(id);

            return _mapper.Map<Member>(user);
        }

        public async Task<bool> SetMainPhoto(int photoId, string username)
        {
            // DO to EF track changes it will update the entities itself
            var user = await _unitOfWork.UserRepository.GetAsync(x => x.UserName == username, true, includeProperties: "Photos")
                ?? throw new Exception("Could not find user");

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null || photo.IsMain) throw new Exception("Cannot use this as main photo");

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;

            return await _unitOfWork.Complete();

        }

        /// <summary>
        ///  TODO - Replace this int with any enum,
        ///  NOTE - We need to set traking value to TRUE in GetAsync so that EF can track the entities and update it.
        /// </summary>
        /// <param name="memberUpdate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> UpdateUser(MemberUpdate memberUpdate , string username)
        {
            // DO to EF track changes it will update the entities itself
            var user = await _unitOfWork.UserRepository.GetAsync(x => x.UserName == username, true, includeProperties: "Photos");

            _mapper.Map(memberUpdate, user);

            return  await _unitOfWork.Complete();
        }
    }
}