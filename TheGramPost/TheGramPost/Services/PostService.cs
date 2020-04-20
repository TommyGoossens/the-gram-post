using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog;
using TheGramPost.Helpers;
using TheGramPost.Models;
using TheGramPost.Models.Models;
using TheGramPost.Repository;

namespace TheGramPost
{
    public class PostService : IPostService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IStorageUtility _storageUtil;
        private readonly IFirebaseService _firebaseService;
        private readonly PostContext _repo;
        private readonly IUserContextHelper _userContext;

        public PostService(IStorageUtility storageUtil, IFirebaseService firebaseService, PostContext repo,
            IUserContextHelper userContext)
        {
            _storageUtil = storageUtil;
            _firebaseService = firebaseService;
            _repo = repo;
            _userContext = userContext;
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _repo.Posts.Include(p => p.User).ToListAsync();
        }
        
        public async Task<List<User>> GetAllUsers()
        {
            return await _repo.Users.Include(u => u.Posts).ToListAsync();
        }

        public async Task CreateNewPost(NewPostDTO newPost)
        {
            DateTime timePosted = DateTime.UtcNow;
            var fileTask = _storageUtil.CreateFile(newPost.Image);
            var imageUrlTask = _firebaseService.UploadFile(await fileTask, timePosted);
            var user = await GetUser(_userContext.GetUserId());
            
            Post post = new Post
                {MediaURL = await imageUrlTask, DatePosted = timePosted, Description = newPost.Description, User = user};
            
            user.Posts.Add(post);
            await _repo.Posts.AddAsync(post);
            await _repo.SaveChangesAsync();
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