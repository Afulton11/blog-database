﻿USE BlogDatabase;
GO



-- TODO: add exception handling
CREATE OR ALTER PROCEDURE Blog.CreateOrUpdateUser
	@RoleId AS INT = 0,
	@Username AS NVARCHAR(64),
	@NormalizedUsername AS NVARCHAR(128),
	@Password AS NVARCHAR(128),
	@Email AS NVARCHAR(128),
	@NormalizedEmail AS NVARCHAR(256),
	@IsEmailVerified AS BIT = 0
AS
	BEGIN TRAN
		UPDATE Blog.[User] SET
			Blog.[User].RoleId = @RoleId,
			Blog.[User].NormalizedUsername = @NormalizedUsername,
			Blog.[User].[Password] = @Password,
			Blog.[User].Email = @Email,
			Blog.[User].NormalizedEmail = @NormalizedEmail,
			Blog.[User].IsEmailVerified = @IsEmailVerified,
			Blog.[User].LastUpdatedTime = SYSDATETIME()
		WHERE Blog.[User].Username = @Username

		if @@ROWCOUNT = 0
		BEGIN
			-- default role to user
			SET @RoleId = (
					SELECT r.RoleId
					FROM Blog.[Role] AS r
					WHERE r.[Name] = N'User'
				)

			INSERT INTO Blog.[User] (RoleId, Username, NormalizedUsername, [Password], Email, NormalizedEmail, IsEmailVerified)
			VALUES
				(@RoleId, @Username, @NormalizedUsername, @Password, @Email, @NormalizedEmail, @IsEmailVerified);
		END
	COMMIT TRAN
GO
