USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.FetchRoleById
(
	@RoleId INT
)
AS (
	SELECT *
	FROM Blog.[Role] R
	WHERE R.RoleId = @RoleId
)
GO
