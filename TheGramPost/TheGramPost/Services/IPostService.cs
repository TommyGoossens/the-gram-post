using System.Threading.Tasks;
using TheGramPost.Models.Models;

namespace TheGramPost
{
    public interface IPostService
    {
        Task CreateNewPost(NewPostDTO file);
    }
}