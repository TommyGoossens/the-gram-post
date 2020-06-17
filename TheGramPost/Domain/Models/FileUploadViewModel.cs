using Microsoft.AspNetCore.Http;

namespace TheGramPost.Domain.Models
{
    public class FileUploadViewModel
    {
        public IFormFile File { get; set; }
    }
}