using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using TheGramPost.Models.Models;

namespace TheGramPost
{
    public class PostService
    {
        
        private readonly IStorageUtility _storageService;
        private readonly IFirebaseService _firebaseService;
        private readonly PostService _postService;
        public PostService(IWebHostEnvironment env)
        {
            _storageService = new StorageUtility(env);
            _firebaseService = new FirebaseService();
        }
        public async Task CreateNewPost(NewPostDTO file)
        {
            FileStream fs = null;
            fs = await _storageService.CreateFile(file.Image);
            string imageUrl = await _firebaseService.UploadFile(fs, file.Image.FileName);
            
        }
    }
}