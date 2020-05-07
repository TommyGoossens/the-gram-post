using System;
using System.IO;
using MediatR;

namespace TheGramPost.Domain.Command.UploadFileToFirebase
{
    public class UploadFileToFirebaseCommand : IRequest<string>
    {
        public FileStream FileStream { get; }
        public DateTime TimePosted { get; }

        public UploadFileToFirebaseCommand(FileStream fileStream, DateTime timePosted)
        {
            FileStream = fileStream;
            TimePosted = timePosted;
        }
    }
}