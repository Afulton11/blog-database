using System.Collections.Generic;
using System.Data;

namespace DataAccess.QueryServices.Readers
{
    public interface IReader<TResult>
    {
        IList<TResult> Read(IDataReader reader);
    }
}
