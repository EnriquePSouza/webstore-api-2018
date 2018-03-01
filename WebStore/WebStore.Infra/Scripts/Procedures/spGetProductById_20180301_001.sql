USE webstore
GO
CREATE PROCEDURE spGetProductById
	@Id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT [Id], [Title], [Price],
		[Image], [QuantityOnHand]
		FROM [Product]
		WHERE [Id] = @Id
END