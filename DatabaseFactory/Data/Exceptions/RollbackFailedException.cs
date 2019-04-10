using System;
using System.Data;

namespace DatabaseFactory.Data.Exceptions
{
    public class RollbackFailedException : Exception
    {
        public RollbackFailedException(IDbTransaction transaction, Exception reason)
            : base(
$"The {transaction?.GetType()} failed while performing a Rollback() when a commit failed."
)
        {
            Transaction = transaction;
            Reason = reason;
        }

        /// <summary>
        /// The <see cref="IDbTransaction"/> that failed to successfully perform a Rollback()
        /// </summary>
        public IDbTransaction Transaction { get; }
        /// <summary>
        /// The <see cref="Exception"/> thrown when the Rollback failed for the <see cref="Transaction"/>
        /// </summary>
        public Exception Reason { get; }
    }
}
