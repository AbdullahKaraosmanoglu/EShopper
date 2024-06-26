USE [DbEshopper]
GO
/****** Object:  StoredProcedure [dbo].[SpAddOrdersByUserId]    Script Date: 16.07.2023 20:33:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[SpAddOrdersByUserId]  
(  
   @UserId int,
   @SubTotal decimal(12,2),
   @PaymentType int,
   @Address nvarchar(500),
   @Description nvarchar(500),
   @PhoneNumber nvarchar(10),
   @CreditCardNumber nvarchar(20),
   @CreditCardName nvarchar(50),
   @CreditCartLastDate nvarchar(50),
   @CreditCardSecurityNumber nvarchar(50)
)  
as  
begin  
SET NOCOUNT ON;

   Insert into TblOrders (UserId, TranDate, SubTotal, PaymentType, Address, Description, PhoneNumber, OrderStatus, CreditCardNumber, CreditCardName, CreditCartLastDate, CreditCardSecurityNumber)
   values (@UserId, GETDATE(), @SubTotal, @PaymentType, @Address, @Description, @PhoneNumber, 1, @CreditCardNumber, @CreditCardName, @CreditCartLastDate, @CreditCardSecurityNumber)

   delete from TblCarts where UserId = @UserId
  
    select CAST(SCOPE_IDENTITY() AS int)
End 