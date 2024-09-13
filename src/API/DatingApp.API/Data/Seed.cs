using DatingApp.Framework.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DatingApp.API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            int failed = 0;
            if (await userManager.Users.AnyAsync()) return;

            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            

            var userData = await File.ReadAllTextAsync(Path.GetDirectoryName(path) + @"/UserSeedData.json");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var users = JsonSerializer.Deserialize<List<ApplicationUser>>(userData, options);

            if (users == null) return;

            //    var roles = new List<ApplicationRole>
            //{
            //    new() {Name = "Member"},
            //    new() {Name = "Admin"},
            //    new() {Name = "Moderator"},
            //};

            //    foreach (var role in roles)
            //    {
            //        await roleManager.CreateAsync(role);
            //    }

            
            foreach (var user in users)
            {
                // user.Photos.First().IsApproved = true;
                user.UserName = user.UserName!.ToLower();
               
                   await userManager.CreateAsync(user, "Pa$$w0rd");
               
                // await userManager.AddToRoleAsync(user, "Member");
            }

           

            //var admin = new ApplicationUser
            //{
            //    UserName = "admin",
            //    KnownAs = "Admin",
            //    Gender = "",
            //    City = "",
            //    Country = ""
            //};

            //await userManager.CreateAsync(admin, "Pa$$w0rd");
            //await userManager.AddToRolesAsync(admin, ["Admin", "Moderator"]);
        }
    }
}
