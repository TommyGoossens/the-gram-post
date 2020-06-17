using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TheGramPost.Domain.DTO.Response;
using TheGramPost.Domain.Models;
using TheGramPost.Repository;

namespace TheGramPost.Domain.Query.GetPostsOfFollowedUsers
{
    public class GetPostsOfFollowedUsersHandler : IRequestHandler<GetPostsOfFollowedUsersQuery, List<FeedPostsResponse>>
    {
        private readonly PostContext _postContext;

        public GetPostsOfFollowedUsersHandler(PostContext postContext)
        {
            _postContext = postContext;
        }
        
        public async Task<List<FeedPostsResponse>> Handle(GetPostsOfFollowedUsersQuery request, CancellationToken cancellationToken)
        {
            return await PaginatedList<FeedPostsResponse>.CreateAsync(
                _postContext.Posts
                    .Where(p => request.RequestFollowedUsers.Contains(p.User.UserId))
                    .OrderByDescending(p => p.DatePosted)
                    .Include(p => p.User)
                    .Select(post => new FeedPostsResponse
                    {
                        Comments = post.Comments.Count,
                        Likes = post.Likes.Count,
                        DatePosted = post.DatePosted,
                        MediaURL = post.MediaURL,
                        User = post.User
                    })
                , request.Page, 20);
        }
    }
}