using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TheGramPost.Domain.Command.CreateLocalFileCopy
{
    public class CreateLocalFileCopyHandler : IRequestHandler<CreateLocalFileCopyCommand, FileStream>
    {
        private readonly IStorageUtility _storageUtility;

        public CreateLocalFileCopyHandler(IStorageUtility storageUtility)
        {
            _storageUtility = storageUtility;
        }

        public async Task<FileStream> Handle(CreateLocalFileCopyCommand request, CancellationToken cancellationToken)
        {
            return await _storageUtility.CreateFile(request.File);
        }
    }
}