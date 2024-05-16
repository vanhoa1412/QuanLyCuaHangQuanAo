CREATE DATABASE QUANLYSHOP
GO
 
USE QUANLYSHOP
GO

CREATE TABLE Users (
    Users_Id INT IDENTITY(1,1) PRIMARY KEY,
    Users_Name VARCHAR(150) UNIQUE,
    Users_Email VARCHAR(150),
	Users_Password VARCHAR(150) NOT NULL DEFAULT 0,
	Users_Type VARCHAR(100) DEFAULT N'User'
)
GO

CREATE TABLE Category(
	Category_Id INT IDENTITY(1,1) PRIMARY KEY,
	Category_Name  VARCHAR(100) UNIQUE,
	Category_Status VARCHAR(100),
)
GO

CREATE TABLE Brand(
	Brand_Id INT IDENTITY(1,1) PRIMARY KEY,
	Brand_Name  VARCHAR(100) UNIQUE,
	Brand_Status VARCHAR(100),
)
GO
CREATE TABLE Cloth
(
	Cloth_ID INT IDENTITY PRIMARY KEY,
	Cloth_Name  NVARCHAR(100) ,
	Cloth_Image IMAGE,
	Cloth_Brand INT,
	Cloth_Category INT,
	Cloth_Quantity INT,
	Cloth_Status  NVARCHAR(100) DEFAULT N'',
	Cloth_Price INT ,

	FOREIGN KEY (Cloth_Category) REFERENCES dbo.Category (Category_ID),
	FOREIGN KEY (Cloth_Brand) REFERENCES dbo.Brand (Brand_ID)

);
GO

CREATE TABLE Bill
(
	Bill_Id INT IDENTITY PRIMARY KEY,
	Bill_Date DATE DEFAULT GETDATE(),
	Customer_Name varchar(150),
    Customer_Number varchar(15),
	Bill_Cloth  INT,
	Bill_Quantity int,
	Total_Amount int,
	Discount int,
	Grand_Total float,

	FOREIGN KEY (Bill_Cloth) REFERENCES dbo.Cloth (Cloth_ID)
)
GO
SELECT * FROM Users;
SELECT * FROM Category;
SELECT * FROM Brand;
SELECT * FROM Cloth;
SELECT * FROM Bill;
INSERT INTO Users VALUES
	('1', '1@gmail.com', '1', 'Admin');
Drop Table Category;
Drop Table Brand
Drop Table  Bill;
Drop Table Cloth;

