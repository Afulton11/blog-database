# Blog Database
This repository is a simple skeleton for a blog with a database. Created as a part of a CIS-560 project at Kansas State University.

## Commit Guideline

`Title:` [Project-Name] 'Short Description of what you did'

- Also, reference the issue the commit relates to using "#{issue-number}" that way when we look at the issue, we can see all the commits related to that specific issue.

## Coding Guidelines

`todo: discuss: common coding practices/guidelines so that we all have consistent formatting throughout each commit`

## Dependency Graph

<img alt="Dependency Graph"
     src="https://github.com/Afulton11/blog-database/blob/master/git-resources/Decoupled-Dependency-Graph.png"
     height="300px"/>

## System Design

<img alt="System Design"
     src="https://github.com/Afulton11/blog-database/blob/master/git-resources/Final%20System%20Design.png"/>

## Database Design

`todo: add design diagram and explain it`

## CQRS (Command Query Responsibility Segregation)

**Useful Articles**
- [Martin Fowler on CQRS](https://martinfowler.com/bliki/CQRS.html)
- [...On The Command Side of My Architecture](https://blogs.cuttingedge.it/steven/posts/2011/meanwhile-on-the-command-side-of-my-architecture/)
  - [...On The Query Side of My Architecture](https://blogs.cuttingedge.it/steven/posts/2011/meanwhile-on-the-query-side-of-my-architecture/)

Creating a new Command or Query in C# to interact with the Database
---


### Command
For this example we will create a Command that will create a point for a given userId by calling a procedure in a sql database.

The **_SQL Procedure_** might look something like this:
```sql
CREATE OR ALTER PROCEDURE Blog.CreatePoint
(
     @UserId INT
)
BEGIN
     ...
END
GO
```

- We can see that the procedure uses the SCHEMA `Blog` and is named `CreatePoint`
  - The _full name_ of the query is `Blog.CreatePoint`
- It also has 1 input parameter: `@UserId`
  - This parameter is of type int and is labeled UserId, so it probably wants the Id property on a User i.e. `User.UserId`

#### Command Model

- A _Command Model_ is a POCO (Plain Old Common Language Runtime Object

The **_Command Model_** might look something like this:
```C#
public class CreatePointCommand : ICommand
{
     [Required]
     public int UserId { get; set; }
}
```

- We create a class whose properties are equivalent to the parameters of the _SQL procedure_.
- the UserId uses the `Required` attribute because the _SQL procedure_ will not be able to create a point without it.

#### Command Service

- A _Command Service_ is a simple class that performs an action on the _Command Model_

The **_Command Service_** might look something like this:
```C#
public class CreatePointCommandService : ICommandService<CreatePointCommand>
{
     private readonly IDatabase database;
     
     public CreatePointCommandService(IDatabase database)
     {
          EnsureArg.IsNotNull(database, nameof(database));
          
          this.database = database;
     }
     
     public void Execute(CreatePointCommand command)
     {
          database.TryExecute((transaction) =>
          {
               var procedure = database.CreateStoredProcedure("Blog.CreatePoint", transaction);
               var parameter = database.CreateParameter("@UserId", command.UserId);
               
               procedure.Parameters.Add(parameter);
               
               database.Execute(procedure);
          });
     }
}
```

- First we will create our constructor that takes in a `IDatabase`.
  - this parameter will be injected by our Dependency Injection library at runtime automatically.
- Then, in our Execute method we will _try to execute a transaction_.
  - Using the transaction created, we will then create a call to our stored procedure `Blog.CreatePoint` passing in the _command model's_ properties as the arguments to that procedure.
- Finally, we make sure to execute that procedure call we just created on the database within the scope of the _try execute transaction_.

Using the base class `DbCommandService` we can simplify this class to be the following:
```C#
public class CreatePointCommandService : DbCommandService<CreatePointCommand>
{     
     public CreatePointCommandService(IDatabase database) : base(database)
     {
     }
	 
	 protected override IEnumerable<IDataParameter> GetParameters(CreatePointCommand command)
	 {
		yield return Database.CreateParameter("@UserId", command.UserId);
	 }
	 
	 protected override string ProcedureName => "Blog.CreatePoint"
}
```

### Query

- A **_Query_** is similar to _command_, but returns a result.

For this example we will create a Query that will return a list of articles for a given authorId by calling a procedure in a sql database.

The **_SQL Procedure_** might look something like this:
```sql
CREATE OR ALTER PROCEDURE Blog.GetAuthorArticles
(
     @AuthorId INT,
	 @PageIndex INT,
	 @PageSize INT,
)
BEGIN
     SELECT *
	 FROM Blog.Article Article
	 WHERE Article.AuthorId = @AuthorId
	 ... Paging ...
END
GO
```

- We can see that the procedure uses the SCHEMA `Blog` and is named `GetAuthorArticles`
  - The _full name_ of the query is `Blog.GetAuthorArticles`
- It also has 1 input parameter: `@AuthorId`
  - This parameter is of type int and is labeled AuthorId, so it probably wants the Id property on a Author i.e. `Author.AuthorUserId`

#### Query Model

- A _Query Model_ is a POCO (Plain Old Common Language Runtime Object

The **_Query Model_** might look something like this:
```C#
public class FetchUserArticlesQuery : IQuery<Paged<Article>>
{
     [Required]
     public int AuthorId { get; set; }
	 public PageInfo Paging { get; set; } = new PageInfo();
}
```

- We create a class whose properties are equivalent to the parameters of the _SQL procedure_.
- It implements the `IQuery<Paged<Article>>` interface because this query model will be used to fetch a paginated list of `Article`
- the `AuthorId` uses the `Required` attribute because the _SQL procedure_ will not be able to get a list of articles without it.
- the `Paging` property is added here because we don't always fetch every single article that user has created at once.
  - `Paging` allows us to fetch only a certain amount of articles at a time.

#### Query Service

- A _Query Service_ is a simple class that performs a function on the _Query Model_ and returns a result.

The **_Query Service_** might look something like this:
```C#
public class FetchAuthorArticlesQueryService : IQueryService<FetchUserArticlesQuery, Paged<Article>>
{
     private readonly IDatabase database;
	 private readonly IReader<Article> articleReader;
     
     public FetchAuthorArticlesQueryService(IDatabase database, IReader<Article> articleReader)
     {
          EnsureArg.IsNotNull(database, nameof(database));
          EnsureArg.IsNotNull(articleReader, nameof(articleReader));
          
          this.database = database;
          this.articleReader = articleReader;
     }
     
     public Paged<Article> Execute(FetchUserArticlesQuery query)
     {
          return database.TryExecute((transaction) =>
          {
               var procedure = database.CreateStoredProcedure("Blog.GetAuthorArticles", transaction);
               var authorIdParameter = database.CreateParameter("@AuthorId", query.AuthorId);
               var pageIndexParameter = database.CreateParameter("@PageIndex", query.Paging.PageIndex);
               var pageSizeParameter = database.CreateParameter("@PageSize", query.Paging.PageSize);
               
               procedure.Parameters.Add(authorIdParameter);
               procedure.Parameters.Add(pageIndexParameter);
               procedure.Parameters.Add(pageSizeParameter);
               
               return database.ExecuteReader(procedure, (dataReader) => new Paged<Article>
			   {
				   Paging = query.Paging,
				   Items = articleReader.Read(dataReader)
			   });
          });
     }
}
```

- First we will create our constructor that takes in a `IDatabase` and a `IReader<Article>`.
  - these parameters will be injected by our Dependency Injection library at runtime automatically.
  - the `IReader<Article>` is used to read in Articles from the database into memory.
- Then, in our Execute method we will _try to execute a transaction_.
  - Using the transaction created, we will then create a call to our stored procedure `Blog.GetAuthorArticles` passing in the _query model's_ properties as the arguments to that procedure.
- Finally, we make sure to execute that procedure call we just created on the database within the scope of the _try execute transaction_.
  - This will execute the procedure and read the results using our `articleReader`, then return the new Paged list of Articles.


Using the base class `DbPagedQueryService` we can simplify this class to be the following:
```C#
    public class FetchAuthorArticlesQueryService :
        DbPagedQueryService<FetchArticlesByCategoryQuery, Article>,
        IFetchArticlesByCategoryQueryService
    {
        private readonly IReader<Article> articleReader;
        public FetchAuthorArticlesQueryService(IDatabase database, IReader<Article> articleReader)
            : base(database)
        {
            EnsureArg.IsNotNull(articleReader, nameof(articleReader));

            this.articleReader = articleReader;
        }

        protected override IEnumerable<Article> ReadItems(IDataReader dataReader) =>
            articleReader.Read(dataReader);

        protected override IEnumerable<IDataParameter> GetQueryParameters(FetchArticlesByCategoryQuery query)
        {
            yield return Database.CreateParameter("@AuthorId", query.AuthorId);
        }

        protected override string ProcedureName => "Blog.GetArticlesByCategory";
    }
```

This base class will automatically handle the paginated parameters so they do not need to be added to the `GetQueryParameters` method.

For queries there is another base class `DbQueryService` which is very similar to `DbPagedQueryService` except pagination is not automatically added to the query parameters.
