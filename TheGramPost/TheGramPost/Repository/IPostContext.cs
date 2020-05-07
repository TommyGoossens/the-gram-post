using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheGramPost.Domain.Models;

namespace TheGramPost.Repository
{
    public interface IPostContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<User> Users { get; set; }
        Task SaveChangesAsync();
    }
}