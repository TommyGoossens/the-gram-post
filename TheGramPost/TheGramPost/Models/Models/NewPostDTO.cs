using Microsoft.AspNetCore.Http;

namespace TheGramPost.Models.Models
{
    public class NewPostDTO
    {
        public IFormFile Image { get; set; }
        public string Description { get; set; }

        public NewPostDTO()
        {
        }
    }
}