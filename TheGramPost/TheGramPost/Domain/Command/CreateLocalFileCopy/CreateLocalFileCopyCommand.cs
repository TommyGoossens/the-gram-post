using System.IO;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace TheGramPost.Domain.Command.CreateLocalFileCopy
{
    public class CreateLocalFileCopyCommand : IRequest<FileStream>
    {
        public IFormFile File { get; set; }

        public CreateLocalFileCopyCommand(IFormFile file)
        {
            File = file;
        }
    }
}