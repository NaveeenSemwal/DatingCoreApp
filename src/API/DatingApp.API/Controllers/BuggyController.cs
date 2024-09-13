using DatingApp.Framework.Data.Context;
using DatingApp.Framework.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    public class BuggyController(DataContext context) : BaseApiController
    {
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetAuth()
        {
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<ApplicationUser> GetNotFound()
        {
            var thing = context.Users.Find(-1);

            if (thing == null) return NotFound();

            return thing;
        }

        [HttpGet("server-error")]
        public ActionResult<ApplicationUser> GetServerError()
        {
            var thing = context.Users.Find(-1) ?? throw new Exception("A bad thing has happened");

            return thing;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }
    }
}
