using System.ComponentModel.DataAnnotations;

namespace TheGramPost.Domain.Models
{
    public class Like
    {
        [Key]
        public long Id { get; set; }

        public long PostId { get; set; }
        public string UserId { get; set; }

        public Post Post { get; set; }
    }
}