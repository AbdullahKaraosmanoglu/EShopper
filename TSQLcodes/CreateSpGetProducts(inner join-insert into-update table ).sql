--select * From TblProductBrand
--select*from TblProducts
--select * from TblProductCategory

----insert into TblProducts (ProductCategoryId, ProductBrandId, ProductName, ProductGuid, Price, Stock)
----values (1, 2, 'Adidas Men Sweatspants', '4081de0f-e32f-45d3-a372-4619ebd15277', 33, 50 )

----update TblProducts 

----set ProductCategoryId=2, ProductBrandId=2, ProductName='Nike Women Green Jogger', ProductGuid='39331c85-3082-4852-ae8f-1279d3a364ff', Price=43, Stock=50
----where ProductId=4

----CREATE PROCEDURE SpGetProducts 
----AS 
----BEGIN 
----select p.ProductId,p.ProductCategoryId,p.ProductBrandId,p.ProductName,p.ProductGuid, p.Price,p.Stock,pb.ProductBrandName,pc.ProductCategoryName 
----from TblProducts as p
----inner join TblProductBrand as pb on pb.ProductBrandId=p.ProductBrandId
----inner join TblProductCategory as pc on pc.ProductCategoryId = p.ProductCategoryId
----END 

--exec dbo.SpGetProducts