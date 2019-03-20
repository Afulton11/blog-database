using System;
using Microsoft.Extensions.Logging;

namespace Blog.Core.BusinessLayer.Responses
{
    public static class ResponseExtensions
    {
        public static void SetError(this IResponse response, Exception ex, ILogger logger)
        {
            response.DidError = true;

            if (ex is BlogException blogEx)
            {
                logger?.LogCritical(blogEx.ToString());
                response.ErrorMessage = "There was an internal error.";
            }
            else
            {
                logger?.LogError(ex.Message);
                response.ErrorMessage = ex.Message;
            }
        }
    }
}
