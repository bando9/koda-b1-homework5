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

INSERT INTO dbo.Category (Name)
VALUES 
(N'Bakso'),
(N'Mie Ayam'),
(N'Minuman');

SELECT * from dbo.Category;

USE resto_db;
GO

INSERT INTO dbo.Category (Name)
VALUES 
    (N'Bakso'),
    (N'Mie Ayam'),
    (N'Minuman');
GO

INSERT INTO dbo.Product (CategoryId, Name, Price, Stock, IsActive)
VALUES 
    (1, N'Bakso Urat', 13000, 4850, 1),
    (1, N'Bakso Tetelan', 16000, 3200, 1),
    (1, N'Bakso Iga', 18000, 1950, 1),
    (1, N'Bakso Rudal', 20000, 2400, 1),
    (1, N'Bakso Telor', 20000, 2850, 1),
    (1, N'Bakso Satelit', 22000, 1100, 1),
    (1, N'Bakso Rusuk', 28000, 850, 1),
    (1, N'Bakso Nuklir 500gr', 45000, 420, 1),

    (2, N'Mie Ayam', 12000, 6200, 1),
    (2, N'Mie Ayam Bakso', 15000, 5400, 1),
    (2, N'Mie Ayam Balungan', 15000, 1750, 1),
    (2, N'Mie Ayam Telor', 15000, 2100, 1),
    (2, N'Mie Ayam Ceker 250gr', 16000, 2900, 1),
    (2, N'Mie Ayam Kepala 250gr', 16000, 1350, 1);
GO

SELECT Id, Name, Price from dbo.Product;

CREATE INDEX IDX_Product_CategoryId ON dbo.Product(CategoryId)
GO;

Select * FROM sys.tables;