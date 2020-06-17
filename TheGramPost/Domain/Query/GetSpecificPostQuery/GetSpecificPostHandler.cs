using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TheGramPost.Domain.DTO.Response;
using TheGramPost.Services;

namespace TheGramPost.Domain.Query.GetSpecificPostQuery
{
    public class GetSpecificPostHandler : IRequestHandler<GetSpecificPostQuery,GetSpecificPostResponse>
    {
        private readonly IPostService _postService;

        public GetSpecificPostHandler(IPostService postService)
        {
            _postService = postService;
        }

        public Task<GetSpecificPostResponse> Handle(GetSpecificPostQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}