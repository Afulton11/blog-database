using System.Collections.Generic;
using System.Data;

namespace Core.Business.QueryServices.Readers
{
    public interface IReader<TResult>
    {
        IEnumerable<TResult> Read(IDataReader reader);
    }
}
