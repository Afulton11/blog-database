using Blog.Core.EntityLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Core.DataLayer.Mapping
{
    public static class EntityTypeBuilderExtensions
    {
        public static void ConfigureAuditableEntity<TAuditable>(this EntityTypeBuilder<TAuditable> builder)
            where TAuditable : class, IAuditableEntity
        {
            builder.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
            builder.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
            builder.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
            builder.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");
        }

        public static void ConfigureConcurrentEntity<TConcurrent>(this EntityTypeBuilder<TConcurrent> builder)
            where TConcurrent : class, IConcurrentEntity
        {
            // set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
