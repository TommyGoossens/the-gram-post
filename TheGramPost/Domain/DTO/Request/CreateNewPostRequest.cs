using MediatR;
using Microsoft.AspNetCore.Http;
using TheGramPost.Domain.DTO.Response;

namespace TheGramPost.Domain.DTO.Request
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