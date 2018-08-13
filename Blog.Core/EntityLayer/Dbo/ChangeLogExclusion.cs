using System;

namespace Blog.Core.EntityLayer.Dbo
{
    public class ChangeLogExclusion : IEntity
    {
        public Int32? ChangeLogExclusionID { get; set; }

        public string EntityName { get; set; }

        public string PropertyName { get; set; }
    }
}
