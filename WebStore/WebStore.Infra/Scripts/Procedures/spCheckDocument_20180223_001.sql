USE webstore
GO
CREATE PROCEDURE spCheckDocument
	@Document CHAR(11)
AS
	SELECT CASE WHEN EXISTS (
		SELECT [Id]
		FROM [Customer]
		WHERE [DocumentNumber] = @Document
	)
	THEN CAST(1 AS BIT)
	ELSE CAST(0 AS BIT) END

