using System.IO;
using System.Threading.Tasks;

namespace TheGramPost
{
    public interface IFirebaseService
    {
        public Task<string> UploadFile(FileStream file, string fileName);
    }
}