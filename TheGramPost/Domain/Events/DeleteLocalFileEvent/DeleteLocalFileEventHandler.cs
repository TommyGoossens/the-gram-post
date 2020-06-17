using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TheGramPost.Domain.Events.DeleteLocalFileEvent
{
    public class DeleteLocalFileEventHandler : INotificationHandler<DeleteLocalFileEvent>
    {
        private readonly IStorageUtility _storageUtility;

        public DeleteLocalFileEventHandler(IStorageUtility storageUtility)
        {
            _storageUtility = storageUtility;
        }

        public async Task Handle(DeleteLocalFileEvent notification, CancellationToken cancellationToken)
        {
            await _storageUtility.DeleteFile(notification.FileToBeDeleted);
        }
    }
}