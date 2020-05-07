using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TheGramPost.Domain.Models.DTO;
using TheGramPost.Services;

namespace TheGramPost.Domain.Command.NewPostCommand
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostCreatedResponse>
    {
        private readonly IPostService _postService;

        public CreatePostCommandHandler(IPostService postService)
        {
            _postService = postService;
        }
        
        public async Task<PostCreatedResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            return await _postService.CreateNewPost(request);
        }
    }
}