using DatingApp.Framework.Data.Context;
using DatingApp.Framework.Data.Interfaces;
using DatingApp.Framework.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Data.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dbContext;
        private readonly ILoggerFactory _logger;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public UnitOfWork(DataContext dbContext, ILoggerFactory logger, IConfiguration configuration = null, 
            UserManager<ApplicationUser> userManager = null)
        {
            _dbContext = dbContext;
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
        }
        //public IBooksRepository BooksRepository => new BooksRepository(_dbContext, _logger);

        //public IRolesRepository RolesRepository => new RolesRepository(_dbContext, _logger);

        public IUserRepository UserRepository => new UserRepository(_dbContext, _logger);

        public async Task<bool> Complete()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _dbContext.ChangeTracker.HasChanges();
        }
    }
}
