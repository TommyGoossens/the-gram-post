using System;
using Microsoft.AspNetCore.Mvc;

namespace TheGramPost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        [HttpPost]
        public ActionResult PlaceCommentOnPost()
        {
            throw new NotImplementedException();
        }
    }
}