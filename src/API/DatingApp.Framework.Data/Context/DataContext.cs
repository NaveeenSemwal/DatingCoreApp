using DatingApp.Framework.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Data.Context
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ApplicationUser> Users { get; set; }
    }
}
