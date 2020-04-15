using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheGramPost.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AbstractPostController : ControllerBase
    {
    }
}