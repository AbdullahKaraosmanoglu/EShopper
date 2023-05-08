--Create procedure [dbo].[SpAddNewUsers]  
--(  
--   @Name nvarchar (50),
--   @Surname nvarchar (50),
--   @DateOfBirth Date,  
--   @Address nvarchar (250),
--   @Email nvarchar (50),  
--   @Password nvarchar (50)
   
   
--)  
--as  
--begin  
--   Insert into TblUsers values(@Name,@Surname,@Address,@DateOfBirth,@Email,@Password)  
--End 

Create procedure [dbo].[SpAddNewUsers]  
(  
   @Name nvarchar (50),
   @Surname nvarchar (50),
   @DateOfBirth Date,  
   @Address nvarchar (250),
   @Email nvarchar (50),  
   @Password nvarchar (50)
   
   
)  
as  
begin  
   Insert into TblUsers (Name,Surname,Adress,DateOfBirth,Email,Password) values(@Name,@Surname,@Address,@DateOfBirth,@Email,@Password)  
End 
