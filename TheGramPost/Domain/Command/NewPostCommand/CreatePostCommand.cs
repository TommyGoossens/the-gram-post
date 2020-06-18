using MediatR;
using Microsoft.AspNetCore.Http;
using TheGramPost.Domain.DTO;
using TheGramPost.Domain.DTO.Response;

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