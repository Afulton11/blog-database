using Blog.Core.EntityLayer.Dbo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Core.DataLayer.Mapping.Dbo
{
    public sealed class EventLogConfiguration : IEntityTypeConfiguration<EventLog>
    {
        public void Configure(EntityTypeBuilder<EventLog> builder)
        {
            builder.ToTable("EventLog", "dbo");

            builder.HasKey(p => p.EventLogID);

            builder.Property(p => p.EventType).HasColumnType("int").IsRequired();
            builder.Property(p => p.Key).HasColumnType("varchar(255)").IsRequired();
            builder.Property(p => p.Message).HasColumnType("varchar(max)").IsRequired();
            builder.Property(p => p.EntryDate).HasColumnType("datetime").IsRequired();
        }
    }
}
