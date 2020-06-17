using System.Collections.Generic;
using MediatR;
using TheGramPost.Domain.DTO.Response;

namespace TheGramPost.Domain.Query.GetPostsOfFollowedUsers
{
    public class GetPostsOfFollowedUsersQuery : IRequest<List<FeedPostsResponse>>
    {
        public int Page { get; }
        public List<string> RequestFollowedUsers { get; }

        public GetPostsOfFollowedUsersQuery(int page, List<string> requestFollowedUsers)
        {
            Page = page;
            RequestFollowedUsers = requestFollowedUsers;
        }
    }
}