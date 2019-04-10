# ADO.NET-ORM-Less-Database
A ORM-Less library based off of EF Core's DbContext. Made for use in an ASP.NET Core application, but could be used in any .Net Core application.

## Utilizing .Net Core's Dependency Injection
```C#
        public void ConfigureServices(IServiceCollection services)
        {
            ...
            
            // Create a SQLiteDatabase that can be injected into any constructor with a type parameter of 'SQLiteDatabase'.
            services.AddDatabase<SQLiteDatabase>((provider, options) =>
            {
                options.UseSqliteDataSource("some/path/to/sqlite.db");
            });
            
             // Create 'SomeDatabase' that can be injected into any constructor with a type parameter of 'ISomeContract'.
            services.AddDatabase<ISomeContract, SomeDatabase>((provider, options) =>
            {
                options.UseConnectionString("some/connection/string");
            });
            ...
        }
```

While there is a built in `SQLiteDatabase` and `SQLDatabase` You may extend those or create a new custom Database altogether by extending `Database`
