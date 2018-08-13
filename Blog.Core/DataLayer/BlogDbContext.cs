using LiteGuard;
using Microsoft.EntityFrameworkCore;

namespace Blog.Core.DataLayer
{
    public sealed class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions options)
            : base(options)
        {
            Guard.AgainstNullArgument(nameof(options), options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyBlogConfigurations(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void ApplyBlogConfigurations(ModelBuilder modelBuilder)
        {
            /* ...Apply mapping all of our POCOs here ... */
        }
    }
}
