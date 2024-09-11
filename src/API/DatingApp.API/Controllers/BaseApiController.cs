using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [ApiController]
   // [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json", new string[] { })]
    public abstract class BaseApiController : ControllerBase
    {

    }
}
