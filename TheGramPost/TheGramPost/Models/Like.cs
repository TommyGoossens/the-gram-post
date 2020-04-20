using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheGramPost.Models
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