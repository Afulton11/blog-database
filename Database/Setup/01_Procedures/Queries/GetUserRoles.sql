USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.GetUserRoles
	@UserId AS INT
AS
	SELECT r.[Name] AS UserRole
	FROM Blog.[User] AS u
		INNER JOIN Blog.[Role] AS r
			ON r.RoleId = u.RoleId
	WHERE u.UserId = @UserId;
GO