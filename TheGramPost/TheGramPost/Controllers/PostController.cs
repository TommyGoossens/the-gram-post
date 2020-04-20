using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheGramPost.Models;
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
        [HttpPost("test")]
        public async Task<ActionResult> NewPost([FromForm] NewPostDTO post)
        {
            await _postService.CreateNewPost(post);
            return new OkResult();
        }

        [HttpGet("{id:int}")]
        public string GetSpecificPost(int id)
        {
            Console.WriteLine($"Getting post {id}");
            return $"Getting post {id}";
        }

        [HttpGet("posts")]
        public async Task<List<Post>> GetAllPosts()
        {
            return await _postService.GetAllPosts();
        }

        [HttpGet("users")]
        public async Task<List<User>> GetAllUsers()
        {
            return await _postService.GetAllUsers();
        }
    }
}