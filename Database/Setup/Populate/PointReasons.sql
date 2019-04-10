USE SimpleDatabase;
GO

INSERT Blog.PointReasons (Reason)
VALUES
    (N'Published an article'),
    (N'Favorited a comment'),
    (N'Commented on an article'),
    (N'Replied to a new comment thread'),
    (N'Visited website')

SELECT *
FROM Blog.PointReasons;