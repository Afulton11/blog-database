USE BlogDatabase;
GO


CREATE OR ALTER PROCEDURE Blog.FetchUserByNormalizedEmail
	@NormalizedEmail NVARCHAR(128)
AS
	SELECT *
	FROM Blog.[User] U
	WHERE U.NormalizedEmail = @NormalizedEmail
GO