using TheGramPost.Services;

namespace TheGramPost.Domain.Query.GetSpecificPostQuery
{
    public class GetSpecificPostHandler
    {
        private readonly IPostService _postService;

        public GetSpecificPostHandler(IPostService postService)
        {
            _postService = postService;
        }
    }
}