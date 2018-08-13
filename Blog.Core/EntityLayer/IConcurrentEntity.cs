using System;

namespace Blog.Core.EntityLayer
{
    public interface IConcurrentEntity
    {
        Byte[] Timestamp { get; set; }
    }
}
