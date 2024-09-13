using DatingApp.Framework.Data.Context;
using DatingApp.Framework.Data.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                         .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
                         {
                             o.RequireHttpsMetadata = false;
                             o.SaveToken = true;

                             o.IncludeErrorDetails = true;
                             var token = configuration["TokenKey"] ?? throw new Exception("TokenKey not found");

                             o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                             {
                                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token)),

                                 ValidateIssuer = false,
                                 ValidateAudience = false,
                                 ValidateLifetime = true,
                                 ValidateIssuerSigningKey = true,
                                 ClockSkew = TimeSpan.Zero
                             };
                         });

            return services;
        }

        public static IServiceCollection ConfigureIdentityCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<ApplicationUser>(opt => opt.User.RequireUniqueEmail = false).AddRoles<ApplicationRole>()
             .AddRoleManager<RoleManager<ApplicationRole>>()
             .AddEntityFrameworkStores<DataContext>();

            return services;
        }
    }
}
