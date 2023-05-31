Create procedure [dbo].[SpAddProduct]  
(  
	  @ProductId int,
      @ProductCategoryId int,
      @ProductBrandId int,
      @ProductName nvarchar (50),
      @ProductGuid uniqueidentifier,
      @Price decimal(12, 2),
      @Stock int
   	  
   
)  
as  
begin  
SET NOCOUNT ON;
   Insert into TblProducts(ProductId,ProductCategoryId,ProductBrandId,ProductName,ProductGuid,Price, Stock) values(@ProductId,@ProductCategoryId,@ProductBrandId,@ProductName,@ProductGuid,@Price, @Stock)  
    /*RETURN SCOPE_IDENTITY()*/
End 
