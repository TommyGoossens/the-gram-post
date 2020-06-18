using System.Collections.Generic;

namespace TheGramPost.Domain.DTO.Request
{
    public class PaginatedFeedPostsRequest
    {
        public int Page { get; set; }
        public List<string> FollowedUsers { get; set; }
    }
}