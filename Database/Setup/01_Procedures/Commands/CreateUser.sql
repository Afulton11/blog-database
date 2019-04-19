USE BlogDatabase;
GO

-- TODO: add exception handling
CREATE OR ALTER PROCEDURE Blog.CreateUser
	@RoleId AS INT = NULL,
	@Username AS NVARCHAR(64),
	@Password AS NVARCHAR(128),
	@Email AS NVARCHAR(128),
	@IsEmailVerified AS BIT = 0
AS
	-- default role to user
	SET @RoleId = ISNULL(@RoleId, 
		(
			SELECT r.RoleId
			FROM Blog.[Role] AS r
			WHERE r.[Name] = N'User'
		)
	)

	INSERT INTO Blog.[User] (RoleId, Username, [Password], Email, IsEmailVerified)
	VALUES
		(@RoleId, @Username, @Password, @Email, @IsEmailVerified);
GO
