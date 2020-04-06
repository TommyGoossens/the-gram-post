using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TheGramPost
{
    public interface IStorageUtility
    {
        public Task<FileStream> CreateFile(IFormFile file);
        public Task DeleteFile(FileStream path);
    }
}