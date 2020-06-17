using System.IO;
using MediatR;

namespace TheGramPost.Domain.Events.DeleteLocalFileEvent
{
    public class DeleteLocalFileEvent : INotification
    {
        public FileStream FileToBeDeleted { get; set; }

        public DeleteLocalFileEvent(FileStream fileToBeDeleted)
        {
            FileToBeDeleted = fileToBeDeleted;
        }
    }
}