using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace TheGramPost
{
    public class StorageUtility: IStorageUtility
    {
        private readonly IWebHostEnvironment _env;

        public StorageUtility(IWebHostEnvironment env)
        {
            this._env = env;
        }
        
        public async Task<FileStream> CreateFile(IFormFile file)
        {
            FileStream fs = null;
            // upload file
            string folderName = "firebaseFiles";
            string path = Path.Combine(_env.ContentRootPath, folderName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fs = new FileStream(Path.Combine(path, file.FileName), FileMode.Create);
            await file.CopyToAsync(fs);
            try
            {
                return new FileStream(Path.Combine(path, file.FileName), FileMode.Open);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task DeleteFile(FileStream path)
        {
            throw new NotImplementedException();
        }
    }
}