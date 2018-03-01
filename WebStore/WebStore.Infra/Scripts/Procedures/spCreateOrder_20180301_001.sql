USE webstore
GO
CREATE PROCEDURE spCreateOrder
    @Id UNIQUEIDENTIFIER,
	@CustomerId UNIQUEIDENTIFIER,
    @CreateDate DATETIME,
    @Status INT
AS
    INSERT INTO [Order] (
        [Id],
		[CustomerId],
        [CreateDate], 
        [Status]
    ) VALUES (
        @Id,
		@CustomerId,
        @CreateDate,
        @Status
    )