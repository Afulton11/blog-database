USE BlogDatabase
GO

CREATE OR ALTER FUNCTION Blog.GetPointReason( 
    @reason NVARCHAR(256)
)
RETURNS INT
WITH SCHEMABINDING AS
BEGIN
    DECLARE @ReasonID AS INT;
    SELECT @ReasonID = PR.PointReasonID
        FROM Blog.PointReasons PR
        WHERE PR.Reason = @reason
    RETURN @ReasonID;
END;
GO

CREATE OR ALTER PROCEDURE Blog.CreatePointForPublishingArticle(
    @userId INT
) 
AS
BEGIN
    DECLARE @ReasonID INT = Blog.GetPointReason(N'Published Article');
    DECLARE @ExpiresOn DATETIMEOFFSET = DATEADD(DAY, 3, SYSDATETIMEOFFSET());
    
    INSERT INTO Blog.Points(UserID, ReasonID, PointValue, ExipresOn)
    VALUES 
        (@userId, @ReasonID, 2, @ExpiresOn)
END;
GO
