USE BlogDatabase;
GO

-- TODO: add exception handling
CREATE OR ALTER PROCEDURE Blog.CreateOrUpdateRole
	@RoleId INT,
	@Name AS NVARCHAR(64),
	@NormalizedUsername AS NVARCHAR(128)
AS
	BEGIN TRAN

		if @RoleId IS NOT NULL
		BEGIN
			UPDATE Blog.[Role] SET
				Blog.[Role].[Name] = @Name,
				Blog.[Role].NormalizedUsername = @NormalizedUsername
			WHERE Blog.[Role].RoleId = @RoleId
		END

		if @@ROWCOUNT = 0
		BEGIN
			INSERT INTO Blog.[Role] ([Name], NormalizedUserame)
			VALUES
				(@Name, @NormalizedUsername);
		END
	COMMIT TRAN
GO
