using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.BusinessLayer
{
    public class BlogException : Exception
    {
        public BlogException() : base()
        {
        }

        public BlogException(string message) : base(message)
        {
        }

        public BlogException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
