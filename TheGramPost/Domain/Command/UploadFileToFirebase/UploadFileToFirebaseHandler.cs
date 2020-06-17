using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TheGramPost.Domain.Command.UploadFileToFirebase
{
    public class UploadFileToFirebaseHandler : IRequestHandler<UploadFileToFirebaseCommand, string>
    {
        private readonly IFirebaseService _firebaseService;

        public UploadFileToFirebaseHandler(IFirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        public async Task<string> Handle(UploadFileToFirebaseCommand request, CancellationToken cancellationToken)
        {
            return await _firebaseService.UploadFile(request.FileStream, request.TimePosted);
        }
    }
}