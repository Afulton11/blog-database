USE BlogDatabase;
GO

-- assumes user with userid exists and no similar (username,email) combinations exist
CREATE OR ALTER PROCEDURE Blog.UpdateUserEmail
	@UserId AS INT,
	@UpdatedEmail AS NVARCHAR(128)
AS
	UPDATE Blog.[User]
		SET
			Email = @UpdatedEmail,
			LastUpdatedTime = SYSDATETIME()
		WHERE UserId = @UserId;
GO
