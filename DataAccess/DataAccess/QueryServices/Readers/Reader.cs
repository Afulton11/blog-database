using System.Collections.Generic;
using System.Data;

namespace DataAccess.QueryServices.Readers
{
    public abstract class Reader<TResult> : IReader<TResult>
    {
        public IList<TResult> Read(IDataReader reader)
        {
            var result = new List<TResult>();
            
            using (reader)
            {
                while (reader.Read() || reader.NextResult())
                {
                    result.Add(ReadRow(reader));
                }
            }

            return result;
        }

        protected abstract TResult ReadRow(IDataRecord row);
    }
}
