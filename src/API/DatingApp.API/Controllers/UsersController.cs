using DatingApp.Framework.Data.Context;
using DatingApp.Framework.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(DataContext context) : ControllerBase
    {

        [HttpGet]
        public  async  Task<ActionResult<IEnumerable<ApplicationUser>>> GetAll()
        {
            return await context.Users.ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApplicationUser>> Get(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
    }
}
