using Assignment3.Model;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.ApplicationContext
{
    public class PostAppContext : DbContext
    {
        public PostAppContext(DbContextOptions<PostAppContext> options) : base(options) { }

        public DbSet<AppUsers> Users { get; set; }
        public DbSet<PostCategories> Categories { get; set; }
        public DbSet<Posts> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUsers>().ToTable("AppUsers");
            modelBuilder.Entity<PostCategories>().ToTable("PostCategories");
            modelBuilder.Entity<Posts>().ToTable("Posts");
        }
    }
}
