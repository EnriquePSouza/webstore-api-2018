USE webstore
GO
CREATE PROCEDURE spGetCustomerById
	@Id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT [Customer].[Id], [Customer].[UserId], [Customer].[FirstName],
		[Customer].[LastName], [Customer].[DocumentNumber], [Customer].[Email],
		[User].[Username], [User].[Password], [User].[Active]
		FROM [Customer]
		WHERE [Customer].[Id] = @Id
		INNER JOIN [User] ON [Customer].[Id] = [User].[CustomerId];
END