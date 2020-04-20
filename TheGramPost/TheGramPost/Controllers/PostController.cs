using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheGramPost.Models.Models;

namespace TheGramPost.Controllers
{
    public class PostController : AbstractPostController
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [Consumes("multipart/form-data")]
        [HttpPost("post")]
        public async Task<ActionResult> NewPost([FromForm] NewPostDTO post)
        {
            await _postService.CreateNewPost(post);
            return new StatusCodeResult(201);
        }

        [HttpGet("{id:int}")]
        public string GetSpecificPost(int id)
        {
            Console.WriteLine($"Getting post {id}");
            return $"Getting post {id}";
        }
    }
}