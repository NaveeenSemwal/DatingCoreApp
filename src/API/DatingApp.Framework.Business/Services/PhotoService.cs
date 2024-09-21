using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.Common.Configuration;
using DatingApp.Framework.Business.Interfaces;
using DatingApp.Framework.Business.Models.Response;
using DatingApp.Framework.Data.Interfaces;
using DatingApp.Framework.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Business.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IUsersService _usersService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhotoService(IUsersService usersService, IOptions<CloudinarySettings> config, IUnitOfWork unitOfWork, IMapper mapper)
        {
            var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);

            _cloudinary = new Cloudinary(acc);
            _usersService = usersService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MemberPhoto> AddPhotoAsync(IFormFile file, string username)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(x => x.UserName == username, true, includeProperties: "Photos")
                ?? throw new Exception("Cannot find user");

            var uploadResult = new ImageUploadResult();

            MemberPhoto memberPhoto = new MemberPhoto();

            if (file.Length > 0)
            {
                uploadResult = await Upload(file);

                var photo = new Photo
                {
                    Url = uploadResult.SecureUrl.AbsoluteUri,
                    PublicId = uploadResult.PublicId
                };

                if (uploadResult.Error != null)
                    throw new Exception(uploadResult.Error.Message);
                
                    user.Photos.Add(photo);

                    if (await _unitOfWork.Complete())
                    {
                        memberPhoto = _mapper.Map<MemberPhoto>(photo);
                    } 
            }
            return memberPhoto;
        }

        private async Task<ImageUploadResult> Upload(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation()
                    .Height(500).Width(500).Crop("fill").Gravity("face"),
                Folder = "da-net8"
            };

            ImageUploadResult uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}
