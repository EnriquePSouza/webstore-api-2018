USE webstore
GO
CREATE PROCEDURE spCreateCustomer
    @Id UNIQUEIDENTIFIER,
	@UserId UNIQUEIDENTIFIER,
    @FirstName VARCHAR(40),
    @LastName VARCHAR(40),
    @Document CHAR(11),
    @Email VARCHAR(160)
AS
    INSERT INTO [Customer] (
        [Id],
		[UserId],
        [FirstName], 
        [LastName], 
        [Document], 
        [Email]
    ) VALUES (
        @Id,
		@UserId,
        @FirstName,
        @LastName,
        @Document,
        @Email
    )