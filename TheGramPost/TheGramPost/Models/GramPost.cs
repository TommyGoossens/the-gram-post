using System;
using System.Collections.Generic;

namespace TheGramPost.Models
{
    public class GramPost
    {
        public DateTime DatePosted { get; set; }
        public long UserId { get; set; }
        public string PictureId { get; set; }
        public List<long> LikedByUsers { get; set; }
        public List<GramComment> Comments { get; set; }
    }
}