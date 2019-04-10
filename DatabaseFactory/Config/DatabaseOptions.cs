using System;
namespace DatabaseFactory.Config
{
    public abstract class DatabaseOptions
    {

        protected DatabaseOptions()
        {
        }

        public string ConnectionString { get; set; }

        /// <summary>
        /// The type of context that these options are for.
        /// </summary>
        public abstract Type ContextType { get; }
    }
}
