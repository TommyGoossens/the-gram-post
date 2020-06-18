using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog;
using TheGramPost.Domain.Command.NewPostCommand;
using TheGramPost.Domain.DTO.Request;
using TheGramPost.Domain.Query.GetSpecificPostQuery;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecificPost(long id)
        {
            var specificPostResponse = await _mediator.Send(new GetSpecificPostQuery(id));
            if(specificPostResponse == null) return new NotFoundResult();
            return new OkObjectResult(specificPostResponse);
        }
    }
}