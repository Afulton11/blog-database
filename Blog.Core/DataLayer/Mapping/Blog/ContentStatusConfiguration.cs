using System;
using System.Collections.Generic;
using System.Text;
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

            // mapping for entity columns
            builder.Property(p => p.ContentStatusName).HasColumnType("varchar(25)").IsRequired();
        }
    }
}
