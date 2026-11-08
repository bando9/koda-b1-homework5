CREATE DATABASE restodb;

CREATE TABLE dbo.Category(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
)
GO

CREATE TABLE dbo.Product(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CategoryId INT NOT NULL,
    ProductCode NVARCHAR(20) NOT NULL UNIQUE,
    Name NVARCHAR(100) NOT NULL, 
    Price DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,

    CONSTRAINT CK_Products_Price CHECK(Price>=0),
    CONSTRAINT CK_Products_Stock CHECK(Stock>=0),
    CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryId) REFERENCES dbo.Category(Id)
)
GO

INSERT INTO dbo.Category (Name)
VALUES 
(N'Bakso')
-- (N'Mie Ayam'),
-- (N'Minuman');

SELECT * from dbo.Category



INSERT INTO dbo.Product (CategoryId, ProductCode, Name, Price, Stock, IsActive)
VALUES  
    (1, 'BKS-01',N'Bakso Urat', 13000, 4850, 1)
    -- (1, N'Bakso Tetelan', 16000, 3200, 1),
    -- (1, N'Bakso Iga', 18000, 1950, 1),
    -- (1, N'Bakso Rudal', 20000, 2400, 1),
    -- (1, N'Bakso Telor', 20000, 2850, 1),
    -- (1, N'Bakso Satelit', 22000, 1100, 1),
    -- (1, N'Bakso Rusuk', 28000, 850, 1),
    -- (1, N'Bakso Nuklir 500gr', 45000, 420, 1),

    -- (2, N'Mie Ayam', 12000, 6200, 1),
    -- (2, N'Mie Ayam Bakso', 15000, 5400, 1),
    -- (2, N'Mie Ayam Balungan', 15000, 1750, 1),
    -- (2, N'Mie Ayam Telor', 15000, 2100, 1),
    -- (2, N'Mie Ayam Ceker 250gr', 16000, 2900, 1),
    -- (2, N'Mie Ayam Kepala 250gr', 16000, 1350, 1);
GO

SELECT Id, Name, Price, Stock, IsActive from dbo.Product;

CREATE INDEX IDX_Product_CategoryId ON dbo.Product(CategoryId)
GO

Select * FROM sys.tables;


SELECT p.Id, p.CategoryId, c.Name as CategoryName, p.Name, p.Price, p.Stock, p.IsActive
            FROM dbo.Product AS p
            JOIN dbo.Category As c ON c.Id = p.CategoryId
            WHERE p.Id = 1;
