USE BlogDatabase;
GO

-- assumes that user with userid exists and is not already deleted
CREATE OR ALTER PROCEDURE Blog.DeleteUser
	@UserId AS INT
AS
	UPDATE Blog.[User]
		SET 
			DeletedAt = SYSDATETIME(),
			LastUpdatedTime = SYSDATETIME()
		WHERE UserId = @UserId;
GO