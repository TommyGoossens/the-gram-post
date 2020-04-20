using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheGramPost.Models
{
    public class Post
    {
        [Key]
        public long Id { get; set; }

        public DateTime DatePosted { get; set; }
        public string Description { get; set; }
        public string MediaURL { get; set; }
        public User User { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }
    }
}