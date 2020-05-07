using MediatR;
using Microsoft.AspNetCore.Http;

namespace TheGramPost.Domain.Models.DTO
{
    public class CreateNewPostRequest : IRequest<PostCreatedResponse>
    {
        public IFormFile Image { get; set; }
        public string Description { get; set; }

        public CreateNewPostRequest()
        {
        }
    }
}