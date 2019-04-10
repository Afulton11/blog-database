namespace DatabaseFactory.Config.Builder
{
    public interface IDatabaseOptionsBuilder
    {
        /// <summary>
        /// Builds a SQLite connection string from the given dataSource.
        /// Creates the source file if it doesn't already exist.
        /// </summary>
        /// <param name="dataSource">The path to the sqlite data source</param>
        void UseSqliteDataSource(string dataSource);

        /// <summary>
        /// Builds a SQL Server database isntance from the given connectionString.
        /// <param name="connectionString">The url to connect to the sql database</param>
        void UseConnectionString(string connectionString);
    }
}
