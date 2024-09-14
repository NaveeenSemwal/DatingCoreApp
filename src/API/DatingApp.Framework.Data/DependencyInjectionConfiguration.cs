using DatingApp.Framework.Data.Context;
using DatingApp.Framework.Data.Interfaces;
using DatingApp.Framework.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Data
{
    public static class DependencyInjectionConfiguration
    {
        /// <summary>Configure App repositories</summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">Application configuration</param>
        public static IServiceCollection ConfigureAppRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<DataContext>(o => o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //services.AddSingleton<DapperContext>();
            return services;
        }
    }
}
