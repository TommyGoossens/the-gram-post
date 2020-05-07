using MediatR;
using Microsoft.AspNetCore.Http;
using TheGramPost.Domain.Models.DTO;

namespace TheGramPost.Domain.Command.NewPostCommand
{
    public class CreatePostCommand : IRequest<PostCreatedResponse>
    {
        public IFormFile Image { get; set; }
        public string Description { get; set; }

        public CreatePostCommand()
        {
            
        }
    }
}