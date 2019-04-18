using System.Collections.Generic;
using System.Data;

namespace DataAccess.QueryServices.Readers
{
    public interface IReader<TResult>
    {
        IEnumerable<TResult> Read(IDataReader reader);
    }
}
