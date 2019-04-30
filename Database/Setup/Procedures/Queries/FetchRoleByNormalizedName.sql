USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.FetchRoleByNormalizedName
(
	@NormalizedName INT
)
AS (
	SELECT *
	FROM Blog.[Role] R
	WHERE R.NormalizedName = @NormalizedName
)
GO
