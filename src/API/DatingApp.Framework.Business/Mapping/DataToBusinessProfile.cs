using AutoMapper;
using DatingApp.Common.Helpers;
using DatingApp.Framework.Business.Models.Response;
using DatingApp.Framework.Data.Model;

namespace Books.Portal.Framework.Business.Mapping
{
    public class DataToBusinessProfile : Profile
    {
        public DataToBusinessProfile()
        {
            //CreateMap<Books.Data.Model.Book, Books.Business.Model.Book>()
            //    .ForMember(dest => dest.Author, opt => opt.MapFrom(src =>
            //        $"{src.Author.FirstName} {src.Author.LastName}"));

            CreateMap<Photo, MemberPhoto>();

            //CreateMap<(ApplicationUser LocalUser, string Token), LoginResponseDto>()
            //     .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.LocalUser))
            //     .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token));

            //CreateMap<Data.Model.ApplicationUser, RegisterationResponsetDto>();

            CreateMap<ApplicationUser, Member>()
               .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain)!.Url))
               .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));


            // Automapper with inherited list
            CreateMap(typeof(PagedList<>), typeof(PagedList<>)).ConvertUsing(typeof(CustomConverter<,>));
        }
    }
}
