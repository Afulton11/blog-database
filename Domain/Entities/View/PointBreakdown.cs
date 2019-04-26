using Domain.Data;
using Domain.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.View
{
    public class PointBreakdown : IEntity
    {
        public string Reason { get; set; }
        public int PointTotal { get; set; }
        public int ValueTotal { get; set; }
        // ints because I am just using css classes for point graph
        public decimal PointPercentage { get; set; }
        public decimal ValuePercentage { get; set; }
    }
}
