USE BlogDatabase;
GO


CREATE OR ALTER PROCEDURE Blog.FetchUserByNormalizedUsername
	@NormalizedUsername NVARCHAR(128)
AS
	SELECT *
	FROM Blog.[User] U
	WHERE U.NormalizedUsername = @NormalizedUsername
GO