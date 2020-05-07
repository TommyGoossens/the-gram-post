using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TheGramPost.Domain.Models.DTO;
using TheGramPost.Services;

namespace TheGramPost.Domain.Query.GetUserPostsPreviewQuery
{
    public class GetUserPostsPreviewQueryHandler : IRequestHandler<GetUserPostsPreviewQuery, List<PostPreviewResponse>>
    {
        private readonly IPostService _postService;
        public GetUserPostsPreviewQueryHandler(IPostService postService)
        {
            _postService = postService;
        }
        public async Task<List<PostPreviewResponse>> Handle(GetUserPostsPreviewQuery request, CancellationToken cancellationToken)
        {
            return await _postService.GetUserPostPreviews(request.UserId);
        }
    }
}