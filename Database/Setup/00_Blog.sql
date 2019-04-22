DROP DATABASE IF EXISTS BlogDatabase;
GO

CREATE DATABASE BlogDatabase;
GO

USE BlogDatabase;
GO

-- drop all procedures from the database (!danger!)
create Procedure [dbo].[DeleteAllProcedures]
As 
declare @schemaName varchar(500)    
declare @procName varchar(500)
declare cur cursor
for select s.Name, p.Name from sys.procedures p
INNER JOIN sys.schemas s ON p.schema_id = s.schema_id
WHERE p.type = 'P' and is_ms_shipped = 0 and p.name not like 'sp[_]%diagram%'
ORDER BY s.Name, p.Name
open cur

fetch next from cur into @schemaName,@procName
while @@fetch_status = 0
begin
if @procName <> 'DeleteAllProcedures'
exec('drop procedure ' + @schemaName + '.' + @procName)
fetch next from cur into @schemaName,@procName
end
close cur
deallocate cur


-- drop tables
DROP TABLE IF EXISTS Blog.Point;
DROP TABLE IF EXISTS Blog.Reason;
DROP TABLE IF EXISTS Blog.Comment;
DROP TABLE IF EXISTS Blog.Favorite;
DROP TABLE IF EXISTS Blog.Article;
DROP TABLE IF EXISTS Blog.ContentStatus;
DROP TABLE IF EXISTS Blog.ArticleCategory;
DROP TABLE IF EXISTS Blog.Author;
DROP TABLE IF EXISTS Blog.[User];
DROP TABLE IF EXISTS Blog.[Role];
GO

DROP SCHEMA IF EXISTS Blog;
GO

CREATE SCHEMA Blog;
GO

CREATE TABLE Blog.[Role]
(
	RoleId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] NVARCHAR(64) NOT NULL,
	NormalizedName NVARCHAR(128),

	UNIQUE([Name])
);

CREATE TABLE Blog.[User]
(
	UserId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	RoleId INT NOT NULL FOREIGN KEY REFERENCES Blog.[Role] (RoleId),
	Username NVARCHAR(64) NOT NULL,
	NormalizedUsername NVARCHAR(128),
	[Password] NVARCHAR(128) NOT NULL,
	Email NVARCHAR(128) NOT NULL,
	NormalizedEmail NVARCHAR(256),
	IsEmailVerified BIT NOT NULL DEFAULT(0),
	CreationDateTime DATETIME NOT NULL DEFAULT(SYSDATETIME()),
	LastUpdatedTime DATETIME NOT NULL DEFAULT(SYSDATETIME()),
	DeletedAt DATETIME DEFAULT(NULL),
	
	UNIQUE(Username, Email)
);

CREATE TABLE Blog.Author
(
	AuthorUserId INT NOT NULL FOREIGN KEY REFERENCES Blog.[User] (UserId) PRIMARY KEY,
	FirstName NVARCHAR(64) NOT NULL,
	MiddleName NVARCHAR(64),
	LastName NVARCHAR(64) NOT NULL,
	BirthDate DATE NOT NULL,
	DeletedAt DATETIME DEFAULT(NULL)
);

CREATE TABLE Blog.ContentStatus
(
	ContentStatusId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] NVARCHAR(28) NOT NULL,
	DeletedAt DATETIME DEFAULT(NULL),

	UNIQUE([Name])
);

CREATE TABLE Blog.ArticleCategory
(
	ArticleCategoryId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] NVARCHAR(28) NOT NULL,
	CreationUserId INT NOT NULL FOREIGN KEY REFERENCES Blog.[User] (UserId),
	CreationDateTime DATETIME NOT NULL DEFAULT(SYSDATETIME()),
	LastUpdatedDateTime DATETIME NOT NULL DEFAULT(SYSDATETIME()),
	DeletedAt DATETIME DEFAULT(NULL),

	UNIQUE([Name])
);

CREATE TABLE Blog.Article
(
	ArticleId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	AuthorId INT NOT NULL FOREIGN KEY REFERENCES Blog.Author (AuthorUserId),
	Title NVARCHAR(128) NOT NULL,
	[Description] NVARCHAR(512) NOT NULL,
	Body NVARCHAR(2048) NOT NULL,
	ContentStatusId INT NOT NULL FOREIGN KEY REFERENCES Blog.ContentStatus (ContentStatusId),
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES Blog.ArticleCategory (ArticleCategoryId),
	CreationDateTime DATETIME NOT NULL DEFAULT(SYSDATETIME()),
	LastUpdatedDateTime DATETIME NOT NULL DEFAULT(SYSDATETIME()),
	DeletedAt DATETIME DEFAULT(NULL),

	UNIQUE(AuthorId, Title)
);

CREATE TABLE Blog.Favorite
(
	UserId INT NOT NULL FOREIGN KEY REFERENCES Blog.[User] (UserId),
	ArticleId INT NOT NULL FOREIGN KEY REFERENCES Blog.Article (ArticleId),
	DeletedAt DATETIME DEFAULT(NULL),

	UNIQUE(UserId, ArticleId)
);

CREATE TABLE Blog.Comment
(
	CommentId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ParentCommentId INT FOREIGN KEY REFERENCES Blog.Comment (CommentId) DEFAULT(NULL),
	UserId INT NOT NULL FOREIGN KEY REFERENCES Blog.[User] (UserId),
	ArticleId INT NOT NULL FOREIGN KEY REFERENCES Blog.Article (ArticleId),
	Body NVARCHAR (2048) NOT NULL,
	CreationDateTime DATETIME NOT NULL DEFAULT(SYSDATETIME()),
	LastUpdatedDateTime DATETIME NOT NULL DEFAULT(SYSDATETIME()),
	DeletedAt DATETIME DEFAULT(NULL)
);

CREATE TABLE Blog.Reason
(
	ReasonId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Reason NVARCHAR(64) NOT NULL,

	UNIQUE(Reason)
);

CREATE TABLE Blog.Point
(
	PointId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Blog.[User] (UserId),
	ReasonId INT NOT NULL FOREIGN KEY REFERENCES Blog.Reason (ReasonId),
	[Value] INT NOT NULL,
	CreatedAt DATETIME NOT NULL DEFAULT(SYSDATETIME()),
	-- TODO: default expire time?
	ExpiresAt DATETIME DEFAULT(NULL)
);