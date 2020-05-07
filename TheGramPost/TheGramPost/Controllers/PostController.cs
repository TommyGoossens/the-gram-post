using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog;
using TheGramPost.Domain.Command.NewPostCommand;
using TheGramPost.Domain.Models.DTO;
using TheGramPost.Services;

namespace TheGramPost.Controllers
{
    public class PostController : AbstractPostController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMediator _mediator;
        public PostController(IPostService postService, IMediator mediator)
        {
            _mediator = mediator;
        }

        [Consumes("multipart/form-data")]
        [HttpPost("post")]
        public async Task<IActionResult> NewPost([FromForm] CreateNewPostRequest post)
        {
            var createPostCommand = new CreatePostCommand
            {
                Description = post.Description,
                Image = post.Image
            };
            var postCreatedResponse = await _mediator.Send(createPostCommand);
            return new CreatedResult(postCreatedResponse.MediaURL,postCreatedResponse);
        }

        [HttpGet("{id:int}")]
        public string GetSpecificPost(int id)
        {
            Console.WriteLine($"Getting post {id}");
            return $"Getting post {id}";
        }
    }
}