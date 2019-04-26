using Domain.Data;
using Domain.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Blog
{
    public class PointBreakdown : IEntity
    {
        public string Reason { get; set; }
        public int ValueTotal { get; set; }
        public decimal ValuePercentage { get; set; }
    }
}
