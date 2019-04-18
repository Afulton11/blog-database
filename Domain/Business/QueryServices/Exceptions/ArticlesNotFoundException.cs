using System;

namespace Domain.Business.QueryServices.Exceptions
{
    public class ArticlesNotFoundException : Exception
    {
        public ArticlesNotFoundException(DateTimeOffset startDate, DateTimeOffset endDate)
            : base(
$"No articles were found in the database between ({startDate.ToString()}) and ({endDate.ToString()})"
                  )
        {
        }
    }
}
