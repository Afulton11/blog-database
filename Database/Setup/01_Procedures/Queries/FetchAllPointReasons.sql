USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.FetchAllPointReasons
AS
    SELECT *
    FROM Blog.Reason;
GO
