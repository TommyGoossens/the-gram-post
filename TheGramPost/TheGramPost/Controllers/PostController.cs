using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheGramPost.Models;
using TheGramPost.Models.Models;

namespace TheGramPost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;
        
        public PostController(IWebHostEnvironment env)
        {
            _postService = new PostService(env);
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