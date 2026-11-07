CREATE DATABASE resto_db;
USE resto_db;

CREATE TABLE dbo.Category(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME2(0) NOT NULL DEFAULT GETDATE()
)
GO;

CREATE TABLE dbo.Product(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CategoryId INT NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL,
    CreatedAt DATETIME2(0) NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2(0),

    CONSTRAINT CK_Products_Price CHECK(Price>=0),
    CONSTRAINT CK_Products_Stock CHECK(Stock>=0),
    CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryId) REFERENCES dbo.Category(Id)
);


CREATE TABLE dbo.Cart(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CreatedAt DATETIME2(0) NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2(0) 
)
GO;

CREATE TABLE dbo.CartItem(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    CartId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    SubTotal DECIMAL(18,2) NOT NULL,
    CreatedAt DATETIME2(0) NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_CartItem_Cart FOREIGN KEY (CartId) REFERENCES dbo.Cart(Id),
    CONSTRAINT FK_CartItem_Product FOREIGN KEY (ProductId) REFERENCES dbo.Product(Id)
)
GO;

CREATE INDEX IDX_CartItem_ProductId ON dbo.CartItem(ProductId)
CREATE INDEX IDX_Product_CategoryId ON dbo.Product(CategoryId)
GO;

Select * FROM sys.tables;