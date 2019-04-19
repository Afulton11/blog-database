USE BlogDatabase;
GO

-- i don't think this will be necessary but is easy to implement
-- assumes user exists and (username,email) combination don't exist
CREATE OR ALTER PROCEDURE Blog.UpdateUsername
	@UserId AS INT,
	@UpdatedUsername AS NVARCHAR(64)
AS
	UPDATE Blog.[User]
		SET 
			Username = @UpdatedUsername,
			LastUpdatedTime = SYSDATETIME()
		WHERE UserId = @UserId;
GO
