USE webstore
GO
CREATE PROCEDURE spCreateOrderItem
    @Id UNIQUEIDENTIFIER,
	@OrderId UNIQUEIDENTIFIER,
	@ProductId UNIQUEIDENTIFIER,
    @Quantity DECIMAL(10, 2),
    @Price MONEY
AS
    INSERT INTO [OrderItem] (
        [Id],
		[OrderId],
        [ProductId], 
        [Quantity], 
        [Price]
    ) VALUES (
        @Id,
		@OrderId,
        @ProductId,
        @Quantity,
        @Price
    )