using Blog.Core.DataLayer.Mapping.Blog;
using Blog.Core.DataLayer.Mapping.Dbo;
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
            // Apply dbo configurations 
            modelBuilder
                .ApplyConfiguration(new ChangeLogConfiguration())
                .ApplyConfiguration(new ChangeLogExclusionConfiguration())
                .ApplyConfiguration(new EventLogConfiguration());

            // Apply Blog configurations
            modelBuilder
                .ApplyConfiguration(new AuthorConfiguration())
                .ApplyConfiguration(new ArticleConfiguration())
                .ApplyConfiguration(new ArticleCategoryConfiguration())
                .ApplyConfiguration(new ContentStatusConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
