USE webstore
GO
CREATE PROCEDURE spGetCustomerById
	@Id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT [Customer].[Id], [Customer].[FirstName], [Customer].[LastName], 
		[Customer].[Document], [Customer].[Email], [Customer].[UserId],
		[User].[Username], [User].[Password], [User].[Active]
		FROM [Customer]
		INNER JOIN [User] ON [User].[Id] = [Customer].[UserId]
		WHERE [Customer].[Id] = @Id
END