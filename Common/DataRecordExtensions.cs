using EnsureThat;
using System.Data;

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
            EnsureArg.IsNotNullOrEmpty(name);

            var ordinal = record.GetOrdinal(name);

            if (ordinal >= 0)
            {
                return (TResult)record.GetValue(ordinal);
            }

            return default;
        }
    }
}
