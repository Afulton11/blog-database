using Blog.Core.EntityLayer.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Core.DataLayer.Mapping.Blog
{
    public sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            // set mapping to table
            builder.ToTable("Author", "Blog");

            // set key for entity
            builder.HasKey(p => p.AuthorID);

            // set identity for entity (auto-increment)
            builder.Property(p => p.AuthorID).UseSqlServerIdentityColumn();

            builder.Property(p => p.FirstName).HasColumnType("varchar(50)").IsRequired();
            builder.Property(p => p.MiddleName).HasColumnType("varchar(50)");
            builder.Property(p => p.LastName).HasColumnType("varchar(50)");
            builder.Property(p => p.BirthDate).HasColumnType("datetime").IsRequired();
            builder.ConfigureAuditableEntity();
            builder.ConfigureAuditableEntity();
        }
    }
}
