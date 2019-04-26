using EnsureThat;
using System;
using System.Data;
using System.Linq;

namespace Common
{
    public static class DataRecordExtensions
    {
        /// <summary>
        /// Safely returns a specific column by name in a <see cref="IDataRecord"/>.
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="record">The <see cref="IDataRecord"/> to read</param>
        /// <param name="name">The column name to get</param>
        /// <returns>
        /// If the column <paramref name="name"/> is not found, 
        /// <a href="!:https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/default-value-expressions">default</a> is returned.
        /// Otherwise, the column value is returned.
        /// </returns>
        public static TResult GetSafely<TResult>(this IDataRecord record, string name)
        {
            EnsureArg.IsNotNull(record, nameof(record));
            EnsureArg.IsNotNullOrEmpty(name);
            name = name.ToLower();

            var availableNames = Enumerable.Range(0, record.FieldCount).Select(record.GetName);

            foreach (var columnName in availableNames)
            {
                if (columnName.ToLower().Equals(name))
                {
                    var result = record[columnName];

                    if (result != null && result.GetType() != typeof(DBNull))
                    {
                        return (TResult)result;
                    }
                }
            }

            return default;
        }

        public static TResult GetFirstSafely<TResult>(this IDataRecord record)
        {
            EnsureArg.IsNotNull(record, nameof(record));

            if (record.FieldCount > 0)
            {
                return (TResult)record[0];
            }

            return default;
        }
    }
}
