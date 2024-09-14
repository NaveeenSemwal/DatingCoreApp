using DatingApp.Framework.Business.Interfaces;
using DatingApp.Framework.Business.Services;
using DatingApp.Framework.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Business
{
    public static class DependencyInjectionConfiguration
    {
        /// <summary>Configure App Services</summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">Application configuration</param>
        public static void ConfigureAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            // http request should be AddScoped

            //services.AddScoped<IBooksServive, BooksServive>();
            services.AddScoped<IUsersService, UsersService>();
            //services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<ITokenService, TokenService>();
            //services.AddScoped<IPhotoService, PhotoService>();

            //services.Configure<CloudinarySetting>(configuration.GetSection("CloudinarySetting"));
            //services.TryAddSingleton<CloudinarySetting>();

            services.ConfigureAppRepositories(configuration);
        }

    }
}
