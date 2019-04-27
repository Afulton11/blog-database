USE BlogDatabase;
GO

CREATE OR ALTER PROCEDURE Blog.GetPointBreakdownByUserId
    @UserId AS INT
AS
    WITH PointValueTotal (TotalValue)
    AS 
    (
        SELECT SUM(p.[Value])
        FROM Blog.Point AS p
        WHERE p.UserId = @UserId
        GROUP BY p.UserId
    ),
    ReasonValuesTotal (Reason, TotalValue)
    AS
    (
        SELECT
            r.Reason,
            SUM(IIF(p.ReasonId = r.ReasonId, p.[Value], 0))
        FROM Blog.Reason AS r
            LEFT JOIN Blog.Point AS p
                ON p.ReasonId = r.ReasonId
                AND p.UserId = @UserId
        GROUP BY r.Reason
    )
    SELECT
        rvt.Reason AS Reason,
        rvt.TotalValue AS ValueTotal,
        CONVERT(DECIMAL(10,2), (100 * (CAST(rvt.TotalValue AS FLOAT) / CAST(IIF(pvt.TotalValue > 0, pvt.TotalValue, 1) AS FLOAT)))) AS ValuePercentage
    FROM
        PointValueTotal AS pvt,
        ReasonValuesTotal AS rvt
GO