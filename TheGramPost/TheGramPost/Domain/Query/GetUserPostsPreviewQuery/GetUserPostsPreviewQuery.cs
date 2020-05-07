using System.Collections.Generic;
using MediatR;
using TheGramPost.Domain.Models.DTO;

namespace TheGramPost.Domain.Query.GetUserPostsPreviewQuery
{
    public class GetUserPostsPreviewQuery : IRequest<List<PostPreviewResponse>>
    {
        public string UserId { get; set; }
    }
}