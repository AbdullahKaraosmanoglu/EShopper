/*Debug esnas�nda breakpoint ile return.value'nin de�erini yakalamak */
USE [DbEshopper]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[SpLoginControl]
		@Email = N'lale@mail.com',
		@Password = N'1234'

SELECT	'Return Value' = @return_value

GO
