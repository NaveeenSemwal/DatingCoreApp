using CloudinaryDotNet.Actions;
using DatingApp.Framework.Business.Models.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Business.Interfaces
{
    public interface IPhotoService
    {
        Task<MemberPhoto> AddPhotoAsync(IFormFile file, string username);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
