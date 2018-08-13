using System;

namespace Blog.Core.EntityLayer.Dbo
{
    public class EventLog : IEntity
    {
        public Int32? EventLogID { get; set; }

        public Int32? EventType { get; set; }

        public String Key { get; set; }

        public String Message { get; set; }

        public DateTime? EntryDate { get; set; }
    }
}
