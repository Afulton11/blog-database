using System;

namespace Blog.Core.EntityLayer.Blog
{
    public sealed class ContentStatus : IAuditableEntity, IConcurrentEntity
    {
        public ContentStatus()
        {
        }

        public ContentStatus(Int16? contentStatusID)
        {
            ContentStatusID = contentStatusID;
        }

        public Int16? ContentStatusID { get; set; }

        public String ContentStatusName { get; set; }

        public String CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public String LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public Byte[] Timestamp { get; set; }
    }
}
