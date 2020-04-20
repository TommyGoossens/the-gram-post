using System.Collections.Generic;
using System.Threading.Tasks;
using TheGramPost.Models;
using TheGramPost.Models.Models;

namespace TheGramPost
{
    public interface IPostService
    {
        Task CreateNewPost(NewPostDTO newPost);
    }
}