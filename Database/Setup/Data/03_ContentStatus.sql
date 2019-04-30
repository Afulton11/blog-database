USE BlogDatabase;
GO

INSERT INTO Blog.ContentStatus ([Name])
VALUES
	(N'Draft'),
	(N'Published'),
	(N'Deleted'),
	(N'Removed'),
	(N'Archived');