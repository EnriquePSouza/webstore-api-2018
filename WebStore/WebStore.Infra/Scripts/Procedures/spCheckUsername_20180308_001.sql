USE webstore
GO
CREATE PROCEDURE spCheckUsername
	@Username VARCHAR(20)
AS
	SELECT CASE WHEN EXISTS (
		SELECT [Id]
		FROM [User]
		WHERE [Username] = @Username
	)
	THEN CAST(1 AS BIT)
	ELSE CAST(0 AS BIT) END

