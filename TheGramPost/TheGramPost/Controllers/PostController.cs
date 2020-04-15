using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheGramPost.Models.Models;

namespace TheGramPost.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PostController: ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [Consumes("multipart/form-data")]
        [HttpPost("test")]
        public async Task<ActionResult> NewPost()
        {
            if (!(this.Request.ContentLength > 0)) return new BadRequestResult();
            var post = new NewPostDTO(Request.Form);
            await _postService.CreateNewPost(post);

            return new OkResult();
        }

        [HttpGet("{id:int}")]
        public string GetSpecificPost(int id)
        {
            Console.WriteLine($"Getting post {id}");
            return $"Getting post {id}";
        }
    }
}