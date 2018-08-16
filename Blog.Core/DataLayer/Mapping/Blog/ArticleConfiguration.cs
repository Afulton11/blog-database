using Blog.Core.EntityLayer.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Core.DataLayer.Mapping.Blog
{
    public sealed class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            // set mapping for table
            builder.ToTable("Article", "Blog");

            // set key for entity
            builder.HasKey(p => p.ArticleID);

            // set Identity for entity (auto-increment)
            builder.Property(p => p.ArticleID).UseSqlServerIdentityColumn();

            // set mapping for properties
            builder.Property(p => p.ArticleTitle).HasColumnType("nvarchar").IsRequired();
            builder.Property(p => p.ArticleBody).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(p => p.ArticleDescription).HasColumnType("nvarchar(1000)");
            builder.Property(p => p.AuthorID).HasColumnType("bigint").IsRequired();
            builder.Property(p => p.ContentStatusID).HasColumnType("smallint").IsRequired();
            builder.ConfigureAuditableEntity();
            builder.ConfigureConcurrentEntity();

            // setup foreign keys
            builder
                .HasOne(p => p.ContentStatusFk)
                .WithMany(b => b.ContentStatusArticles)
                .HasForeignKey(p => p.ContentStatusID);

            builder
                .HasOne(p => p.AuthorFk)
                .WithMany(b => b.Articles)
                .HasForeignKey(p => p.AuthorID);
        }
    }
}
