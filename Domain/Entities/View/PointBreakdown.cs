using Domain.Data;

namespace Domain.Entities.View
{
    public class PointBreakdown : IEntity
    {
        public string Reason { get; set; }
        public int ValueTotal { get; set; }
        public decimal ValuePercentage { get; set; }
    }
}
