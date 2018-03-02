USE webstore
GO
CREATE PROCEDURE spGetCustomerByUsername
	@Username VARCHAR(20)
AS
BEGIN
	SELECT [Customer].[Id], [Customer].[FirstName], [Customer].[LastName],
		[Customer].[DocumentNumber], [Customer].[Email], [User].[Id],
		[User].[Username], [User].[Password], [User].[Active]
		FROM [Customer]
		INNER JOIN [User] ON [User].[Id] = [Customer].[UserId]
		WHERE [User].[Username] = @Username
END