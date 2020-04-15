using System.Threading.Tasks;

namespace TheGramPost.Helpers
{
    public interface IAuthHelper
    {
        public Task<string> GetAuthToken();
    }
}