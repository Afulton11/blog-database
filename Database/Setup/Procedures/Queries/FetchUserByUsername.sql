USE BlogDatabase
GO


CREATE OR ALTER PROCEDURE Blog.FetchUserByUsername
(
	@Username NVARCHAR(64),
	@WithDeleted BIT = 0
)
AS
BEGIN
	SELECT *
	FROM Blog.[User] U
	WHERE U.Username = @Username
		AND (
			(@WithDeleted = 0 AND U.DeletedAt IS NULL)
			OR @WithDeleted = 1
		)
END
GO