USE [DbEshopper]
GO
/****** Object:  StoredProcedure [dbo].[SpUpdateUsers]    Script Date: 23.09.2023 14:49:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[SpUpdateUsers]  
(  
   @UserId int,	
   @Email nvarchar (50),  
   @Password nvarchar (50),
   @Address nvarchar (250)
)  
as  
begin  
SET NOCOUNT ON;
  update TblUsers set
	   Email=@Email ,  
	   Password=@Password ,
	   Address=@Address 

	   where UserId=@UserId

	   select UserId from TblUsers where UserId=@UserId

End 
