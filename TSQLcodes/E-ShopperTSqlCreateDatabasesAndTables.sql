--CREATE DATABASE DbEshopper;

--CREATE TABLE TblUsers (
--  UserId INT PRIMARY KEY,
--  Name NVARCHAR(50),
--  Surname NVARCHAR(50),
--  Email NVARCHAR(100),
--  Password NVARCHAR(50),
--  DateOfBirth DATE,
--  Gender NVARCHAR(10)
--);

--CREATE TABLE TblProducts (
--	ProductId INT PRIMARY KEY,
--	ProductCategoryId INT,
--	ProductBrandId INT,
--	ProductName NVARCHAR (50),
--	ProductGuid NVARCHAR (50),
--	Price INT,
--	Stock INT,
--	);

--CREATE TABLE TblProductCategory (
--	ProductCategoryId INT PRIMARY KEY,
--	ProductCategoryName NVARCHAR(50),
--	);

--CREATE TABLE TblProductBrand (
--	ProductBrandId INT PRIMARY KEY,
--	ProductBrandName NVARCHAR(50),
--	);

--/*I used the following code line to change the table name that I had misspelled:*/
--exec sp_rename 'dbo.TblProduct', 'dbo.TblProductBrand'

--CREATE TABLE TblCarts (
--	UserId INT PRIMARY KEY,
--	ProductId INT,
--	);

--CREATE TABLE TblOrders (
--	OrdersId INT PRIMARY KEY,
--	UserId Int,
--	RequestDate DATETIME,
--	SubTotal DECIMAL(12,2),
--	);

--CREATE TABLE TblOrderDetails (
--	OrdersId INT PRIMARY KEY,
--	ProductId INT,
--	);