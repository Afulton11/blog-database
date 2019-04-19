using System;
using Domain.Data;
namespace Domain.Entities.Blog
{
    public class Point : IEntity
    {
        public User User { get; }
        public PointReason Reason { get; }
        public int Value { get; }
        public DateTimeOffset CreatedOn { get; }
        public DateTimeOffset ExpiresOn { get; }
    }
}
