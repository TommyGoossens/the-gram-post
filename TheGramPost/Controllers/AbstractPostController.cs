using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheGramPost.Controllers
{
    [Route("api/post")]
    [ApiController]
    [Authorize]
    public class AbstractPostController : ControllerBase
    {
    }
}