USE webstore
GO
CREATE PROCEDURE spCreateUser
    @Id UNIQUEIDENTIFIER,
	@Username VARCHAR(20),
    @Password VARCHAR(32),
    @Active BIT
AS
    INSERT INTO [User] (
        [Id],
		[Username],
        [Password], 
        [Active]
    ) VALUES (
        @Id,
		@Username,
        @Password,
        @Active
    )