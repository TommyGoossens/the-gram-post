using Microsoft.AspNetCore.Http;

namespace TheGramPost.Models
{
    public class FileUploadViewModel
    {
        public IFormFile File { get; set; }
    }
}