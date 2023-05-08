alter PROCEDURE [dbo].[SpLoginControl]
(  
   @Email nvarchar (50),  
   @Password nvarchar (50)
)  
AS
BEGIN
SET NOCOUNT ON;
	SELECT UserId,Name,Surname,Email,Password,DateOfBirth,Gender,Address FROM dbo.TblUsers where Email=@Email AND Password=@Password
END