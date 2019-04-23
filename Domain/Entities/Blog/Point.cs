using System;
using Domain.Data;
namespace Domain.Entities.Blog
{
    public class Point : IEntity
    {
        public int PointId { get; set; }
        public int UserId { get; set; }
        public int ReasonId { get; set; }
        public int Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
