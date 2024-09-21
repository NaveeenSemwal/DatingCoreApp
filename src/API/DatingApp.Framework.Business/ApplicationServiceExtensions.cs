using Asp.Versioning;
using Books.Portal.Framework.Business.Mapping;
using DatingApp.Common.Configuration;
using DatingApp.Common.Helpers;
using DatingApp.Framework.Business.Interfaces;
using DatingApp.Framework.Business.Mapping;
using DatingApp.Framework.Business.Services;
using DatingApp.Framework.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Business
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();


            services.AddAutoMapper(typeof(BusinessToDataProfile),
                typeof(DataToBusinessProfile),
                typeof(CustomConverter<,>));

           

            // Add API versioning
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
           .AddApiExplorer(options =>
            {
             options.GroupNameFormat = "'v'VVV";
             options.SubstituteApiVersionInUrl = true;
            });

            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySetting"));

            return services;
        }
    }
}
