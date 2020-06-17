using System;
using System.Collections.Generic;

namespace TheGramPost.Domain.Models
{
    public class GramPost
    {
        public DateTime DatePosted { get; set; }
        public User User { get; set; }
        public string MediaURL { get; set; }
        public List<long> LikedByUsers { get; set; }
        public List<GramComment> Comments { get; set; }
    }
}