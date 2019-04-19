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

### Query

- A **_Query_** is similar to _command_, but returns a result.

`todo: descriptions + examples like above, but for queries`
