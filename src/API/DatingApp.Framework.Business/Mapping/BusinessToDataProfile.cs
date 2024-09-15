using AutoMapper;
using DatingApp.Framework.Business.Models.Request;
using DatingApp.Framework.Data.Model;

namespace DatingApp.Framework.Business.Mapping
{
    public class BusinessToDataProfile : Profile
    {
        public BusinessToDataProfile()
        {
            //CreateMap<BookForCreation, Data.Model.Book>();

            //CreateMap<JsonPatchDocument<BookForCreation>, JsonPatchDocument<Data.Model.Book>>();

            //CreateMap<PhotoDto, Photo>();

            //CreateMap<LoginRequestDto, Data.Model.ApplicationUser>();

            CreateMap<RegisterationRequest, ApplicationUser>()
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Username + "@gmail.com"));

            //CreateMap<MemberUpdateDto, Data.Model.ApplicationUser>();
        }
        
    }
}
