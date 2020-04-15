using System;
using System.IO;
using System.Threading.Tasks;
using TheGramPost.Models.Models;

namespace TheGramPost
{
    public class PostService : IPostService
    {
        
        private readonly IStorageUtility _storageUtil;
        private readonly IFirebaseService _firebaseService;
        public PostService(IStorageUtility storageUtil, IFirebaseService firebaseService)
        {
            _storageUtil = storageUtil;
            _firebaseService = firebaseService;
        }
        public async Task CreateNewPost(NewPostDTO file)
        {
            DateTime timePosted = DateTime.Now;
            FileStream fs = null;
            fs = await _storageUtil.CreateFile(file.Image);
            string imageUrl = await _firebaseService.UploadFile(fs,timePosted);
            
        }
    }
}