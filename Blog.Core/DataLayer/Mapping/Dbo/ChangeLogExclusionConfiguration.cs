using Blog.Core.EntityLayer.Dbo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Core.DataLayer.Mapping.Dbo
{
    public sealed class ChangeLogExclusionConfiguration : IEntityTypeConfiguration<ChangeLogExclusion>
    {
        public void Configure(EntityTypeBuilder<ChangeLogExclusion> builder)
        {
            builder.ToTable("ChangeLogExclusion", "dbo");

            builder.HasKey(p => p.ChangeLogExclusionID);

            builder.Property(p => p.ChangeLogExclusionID).UseSqlServerIdentityColumn();

            builder.Property(p => p.EntityName).HasColumnType("varchar(128)").IsRequired();
            builder.Property(p => p.PropertyName).HasColumnType("varchar(128)").IsRequired();
        }
    }
}
