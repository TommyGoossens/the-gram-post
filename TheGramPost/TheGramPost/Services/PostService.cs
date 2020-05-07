using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using TheGramPost.Domain.Command.CreateLocalFileCopy;
using TheGramPost.Domain.Command.NewPostCommand;
using TheGramPost.Domain.Command.UploadFileToFirebase;
using TheGramPost.Domain.Events.DeleteLocalFileEvent;
using TheGramPost.Domain.Models;
using TheGramPost.Domain.Models.DTO;
using TheGramPost.Helpers;
using TheGramPost.Repository;

namespace TheGramPost.Services
{
    public class PostService : IPostService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMediator _mediator;
        private readonly IPostContext _repo;
        private readonly IUserContextHelper _userContext;

        public PostService(IMediator mediator, IPostContext repo,
            IUserContextHelper userContext )
        {
            _mediator = mediator;
            _repo = repo;
            _userContext = userContext;
        }
        public async Task<PostCreatedResponse> CreateNewPost(CreatePostCommand request)
        {
            var datePosted = DateTime.UtcNow;

            var fileStream = await _mediator.Send(new CreateLocalFileCopyCommand(request.Image));
            var imageUrlTask = _mediator.Send(new UploadFileToFirebaseCommand(fileStream, datePosted));
            var user = await GetUser(_userContext.GetUserId());

            var post = new Post()
            {
                DatePosted = datePosted,
                Description = request.Description,
                MediaURL = await imageUrlTask,
                User = user
            };

            await _mediator.Publish(new DeleteLocalFileEvent(fileStream));
            
            user.Posts.Add(post);
            await _repo.Posts.AddAsync(post);
            await _repo.SaveChangesAsync();
            
            return new PostCreatedResponse
            {
                Id = post.Id,
                MediaURL = post.MediaURL,
                
            };
        }

        public async Task<List<PostPreviewResponse>> GetUserPostPreviews(string requestUserId)
        {
            return await _repo.Posts.Where(p => p.User.UserId.Equals(requestUserId)).Select(post => new PostPreviewResponse
            {
                Id = post.Id,
                MediaURL = post.MediaURL
            }).ToListAsync();
        }

        public Task<GetSpecificPostResponse> GetSpecifPost(long postId)
        {
            throw new NotImplementedException();
        }

        private async Task<User> GetUser(string userId)
        {
            var user = await _repo.Users.Where(u => u.UserId.Equals(userId)).FirstOrDefaultAsync();
            if (user != null) return user;
            user = new User
            {
                UserId = userId
            };
            await _repo.Users.AddAsync(user);
            return user;
        }
    }
}