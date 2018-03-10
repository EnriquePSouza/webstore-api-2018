USE webstore
GO
CREATE PROCEDURE spGetProductById
	@Id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT [Id], [Title], [Image],
		[Price], [QuantityOnHand]
		FROM [Product]
		WHERE [Id] = @Id
END