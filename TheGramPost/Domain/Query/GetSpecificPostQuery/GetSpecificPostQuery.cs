using MediatR;
using TheGramPost.Domain.DTO.Response;

namespace TheGramPost.Domain.Query.GetSpecificPostQuery
{
    public class GetSpecificPostQuery : IRequest<GetSpecificPostResponse>
    {
        public long PostId { get; }

        public GetSpecificPostQuery(long postId)
        {
            PostId = postId;
        }
    }
}