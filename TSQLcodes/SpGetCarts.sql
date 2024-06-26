USE [DbEshopper]
GO
/****** Object:  StoredProcedure [dbo].[SpGetCarts]    Script Date: 22.06.2023 21:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[SpGetCarts]

	@UserId int
	
as
begin
	select p.ProductName,p.Price,p.Stock,COUNT(p.ProductName) AS 'Quantity',p.ProductGuid  from TblCarts as c
	inner join TblProducts as p on p.ProductId=c.ProductId
	where c.UserId=@UserId
	group by p.ProductName,p.Price,p.Stock,p.ProductGuid

	
end
