using Domain.Data;
using Domain.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.View
{
    /*
     * Point object with reason string
     */
    public class FullPoint : IEntity
    {
        public int PointId { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public int Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
