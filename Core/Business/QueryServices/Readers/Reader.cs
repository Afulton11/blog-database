using System.Collections.Generic;
using System.Data;

namespace Core.Business.QueryServices.Readers
{
    public abstract class Reader<TResult> : IReader<TResult>
    {
        public IEnumerable<TResult> Read(IDataReader reader)
        {
            using (reader)
            {
                while (reader.Read() || reader.NextResult())
                {
                    yield return ReadRow(reader);
                }
            }
        }

        protected abstract TResult ReadRow(IDataRecord row);
    }
}
