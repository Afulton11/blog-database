USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.GetReasonForPoint
	@PointId AS INT
AS
	SELECT *
	FROM Blog.Point AS p
		INNER JOIN Blog.Reason AS r
			ON r.ReasonId = p.ReasonId
	WHERE p.PointId = @PointId
GO
