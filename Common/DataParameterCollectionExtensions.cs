using EnsureThat;
using System.Collections.Generic;
using System.Data;

namespace Common
{
    public static class DataParameterCollectionExtensions
    {
        public static void AddAll(this IDataParameterCollection collection, params IDataParameter[] parameters) =>
            AddAll(collection, parameters);

        public static void AddAll(this IDataParameterCollection collection, IEnumerable<IDataParameter> parameters)
        {
            EnsureArg.IsNotNull(collection, nameof(collection));


            foreach (var p in parameters)
            {
                collection.AddAll(p);
            }
        }
    }
}
