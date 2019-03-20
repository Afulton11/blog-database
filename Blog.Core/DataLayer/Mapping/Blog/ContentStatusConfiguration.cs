using Blog.Core.EntityLayer.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Core.DataLayer.Mapping.Blog
{
    public sealed class ContentStatusConfiguration : IEntityTypeConfiguration<ContentStatus>
    {
        public void Configure(EntityTypeBuilder<ContentStatus> builder)
        {
            // Mapping for table
            builder.ToTable("ContentStatus", "Blog");

            // set entity key
            builder.HasKey(p => p.ContentStatusID);

            // make the id auto increment
            builder.Property(p => p.ContentStatusID).UseSqlServerIdentityColumn();

            // mapping for entity columns
            builder.Property(p => p.ContentStatusName).HasColumnType("varchar(25)").IsRequired();
            builder.ConfigureAuditableEntity();
            builder.ConfigureConcurrentEntity();

            // set unique constraints
            builder
                .HasAlternateKey(p => new { p.ContentStatusName })
                .HasName("U_ContentStatusName");
        }
    }
}
