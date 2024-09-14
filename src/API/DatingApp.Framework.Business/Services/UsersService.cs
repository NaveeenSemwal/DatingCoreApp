using AutoMapper;
using DatingApp.Framework.Business.Interfaces;
using DatingApp.Framework.Business.Models.Request;
using DatingApp.Framework.Business.Models.Response;
using DatingApp.Framework.Data.Interfaces;
using DatingApp.Framework.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}