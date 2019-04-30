USE BlogDatabase;
GO

-- assumes that user with userid exists and is not already deleted
CREATE OR ALTER PROCEDURE Blog.DeleteRole
	@RoleId AS INT
AS
	DELETE FROM Blog.[Role]
	WHERE RoleId = @RoleId
GO
