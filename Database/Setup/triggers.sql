USE BlogDatabase
GO

CREATE OR ALTER TRIGGER Blog.InsertRoleAddNormalizedName 
	ON Blog.[Role]
	AFTER INSERT, UPDATE
AS
BEGIN
	UPDATE R
	SET NormalizedName = UPPER(R.[Name])
	FROM Blog.[Role] R
		INNER JOIN INSERTED RI ON RI.RoleId = R.RoleId
END
GO