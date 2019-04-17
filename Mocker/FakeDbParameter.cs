using System;
using System.Collections.Generic;
using System.Data;

namespace Mocker
{
    public class FakeDbParameter : IDataParameter
    {
        public DbType DbType { get; set; }
        public ParameterDirection Direction { get; set; }
        public bool IsNullable { get; }
        public string ParameterName { get; set; }
        public string SourceColumn { get; set; }
        public DataRowVersion SourceVersion { get; set; }
        public object Value { get; set; }

        public override bool Equals(object obj)
        {
            return obj is FakeDbParameter parameter &&
                   ParameterName == parameter.ParameterName &&
                   EqualityComparer<object>.Default.Equals(Value, parameter.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ParameterName, Value);
        }
    }
}
