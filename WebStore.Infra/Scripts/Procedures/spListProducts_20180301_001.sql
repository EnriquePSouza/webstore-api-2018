USE webstore
GO
CREATE PROCEDURE spListProducts
AS
BEGIN
	SELECT [Id], [Title], [Price], [Image], [QuantityOnHand] 
	FROM [Product]
END

