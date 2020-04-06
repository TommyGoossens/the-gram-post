using System;
using Microsoft.AspNetCore.Mvc;

namespace TheGramPost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        [HttpPost]
        public ActionResult UpdateLike()
        {
            throw new NotImplementedException();
        }
    }
}