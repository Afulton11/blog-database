using System;
using DatabaseFactory.Data;

namespace DatabaseFactory.Config
{
    public class DatabaseOptions<TContext> : DatabaseOptions
        where TContext : Database
    {

        public override Type ContextType => typeof(TContext);
    }
}