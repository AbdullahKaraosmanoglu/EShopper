USE [DbEshopper]
GO
/****** Object:  StoredProcedure [dbo].[SpAddNewUsers]    Script Date: 3.05.2023 16:21:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[SpAddNewUsers]  
(  
   @Name nvarchar (50),
   @Surname nvarchar (50),
   @Email nvarchar (50),  
   @Password nvarchar (50),
   @DateOfBirth Date, 
   @Gender bit,
   @Address nvarchar (250)
)  
as  
begin  
SET NOCOUNT ON;
   Insert into TblUsers (Name,Surname,Email,Password,DateOfBirth,Gender,Address) values(@Name,@Surname,@Email,@Password,@DateOfBirth,@Gender,@Address)  

    RETURN SCOPE_IDENTITY()
End 
