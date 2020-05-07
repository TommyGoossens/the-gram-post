using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheGramPost.Domain.Models;

namespace TheGramPost.Repository
{
    public class PostContext : DbContext, IPostContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<User> Users { get; set; }
        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        public PostContext(DbContextOptions<PostContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User").HasMany(u => u.Posts).WithOne(p => p.User).IsRequired();
            modelBuilder.Entity<Post>().ToTable("Post").HasMany(p => p.Comments).WithOne(c => c.Post);
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Like>().ToTable("Like");
        }
    }
}