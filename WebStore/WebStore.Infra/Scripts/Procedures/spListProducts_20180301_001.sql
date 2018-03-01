USE webstore
GO
CREATE PROCEDURE spListProducts
AS
BEGIN
	SELECT [Id], [Title], [Price], [Image] 
	FROM [Product]
END

