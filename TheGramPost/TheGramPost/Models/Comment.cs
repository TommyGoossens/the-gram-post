using System;
using System.ComponentModel.DataAnnotations;

namespace TheGramPost.Models
{
    public class Comment
    {
        [Key]
        public long Id { get; set; }

        public long PostId { get; set; }
        public string UserId { get; set; }
        public DateTime DatePosted { get; set; }
        public string Message { get; set; }
        public Post Post { get; set; }

        public Comment()
        {
        }
    }
}