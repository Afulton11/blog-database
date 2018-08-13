using Blog.Core.EntityLayer.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Core.DataLayer.Mapping.Blog
{
    public sealed class ArticleCategoryConfiguration : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            // Mapping for the table
            builder.ToTable("ArticleCategory", "Blog");

            // set key for entity
            builder.HasKey(p => p.ArticleCategoryID);

            // make the id auto increment
            builder.Property(p => p.ArticleCategoryID).UseSqlServerIdentityColumn();

            // set mapping for columns
            builder.Property(p => p.ArticleCategoryName).HasColumnName("varchar(25)").IsRequired();
            builder.ConfigureAuditableEntity();
            builder.ConfigureConcurrentEntity();

            // set unique constraints
            builder
                .HasAlternateKey(p => new { p.ArticleCategoryName })
                .HasName("U_ArticleCategoryName");
        }
    }
}
