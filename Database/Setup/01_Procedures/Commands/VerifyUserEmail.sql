USE BlogDatabase;
GO

-- this procedure assumes that the user with userid exists
CREATE OR ALTER PROCEDURE Blog.VerifyUserEmail
	@UserId AS INT
AS
	UPDATE Blog.[User]
		SET 
			IsEmailVerified = 1,
			LastUpdatedTime = SYSDATETIME()
		WHERE UserId = @UserId;
GO