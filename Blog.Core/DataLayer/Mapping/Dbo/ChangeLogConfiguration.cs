using Blog.Core.EntityLayer.Dbo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Core.DataLayer.Mapping.Dbo
{
    public sealed class ChangeLogConfiguration : IEntityTypeConfiguration<ChangeLog>
    {
        public void Configure(EntityTypeBuilder<ChangeLog> builder)
        {
            builder.ToTable("ChangeLog", "dbo");

            builder.HasKey(p => p.ChangeLogID);

            builder.Property(p => p.ChangeLogID).UseSqlServerIdentityColumn();

            builder.Property(p => p.ClassName).HasColumnType("varchar(128)").IsRequired();
            builder.Property(p => p.PropertyName).HasColumnType("varchar(128)").IsRequired();
            builder.Property(p => p.Key).HasColumnType("varchar(255)").IsRequired();
            builder.Property(p => p.OriginalValue).HasColumnType("varchar(max)").IsRequired();
            builder.Property(p => p.CurrentValue).HasColumnType("varchar(max)").IsRequired();
            builder.Property(p => p.UserName).HasColumnType("varchar(25)").IsRequired();
            builder.Property(p => p.ChangeDate).HasColumnType("varchar(128)").IsRequired();

        }
    }
}
