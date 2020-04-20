using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheGramPost.Models
{
    public class User
    {
        [Key] public long Id { get; set; }
        public string UserId { get; set; }

        public string UserName { get; set; }
        public string ProfilePictureURL { get; set; }
        public List<Post> Posts { get; set; }

        public User()
        {
            Posts = new List<Post>();
        }
    }
}