using DatingApp.Common.Helpers;
using DatingApp.Framework.Business.Models.Request;
using DatingApp.Framework.Business.Models.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Business.Interfaces
{
    public interface IUsersService
    {
        //Task<LoginResponse> Login(LoginRequest loginRequestDto);

        //Task<RegisterationResponsetDto> Register(RegisterationRequest registerationRequestDto);

        //Task<PagedList<MemberDto>> GetAll(UserParams searchParams);

        Task<Member> Get(string username);

        Task<Member> Get(object id);

        //Task<bool> UpdateUser(MemberUpdateDto memberUpdateDto);

        //Task<MemberDto> GetUsersWithRoles();

        //Task<PhotoDto> AddPhoto(IFormFile file);
    }
}
