using System.Collections.Generic;
using System.Threading.Tasks;
using TheGramPost.Domain.Command.NewPostCommand;
using TheGramPost.Domain.DTO;
using TheGramPost.Domain.DTO.Response;

namespace TheGramPost.Services
{
    public interface IPostService
    {
        /// <summary>
        /// Creates a new post
        /// </summary>
        /// <param name="request"></param>
        Task<PostCreatedResponse> CreateNewPost(CreatePostCommand request);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestUserId">UserId to fetch</param>
        /// <returns>List of PostPreviewDTOs</returns>
        Task<List<PostPreviewResponse>> GetUserPostPreviews(string requestUserId);

        /// <summary>
        /// Retrieves a specific post with the comments and likes
        /// </summary>
        /// <param name="postId">Id of the requested post</param>
        /// <returns>Post object</returns>
        Task<GetSpecificPostResponse> GetSpecifPost(long postId);
    }
}