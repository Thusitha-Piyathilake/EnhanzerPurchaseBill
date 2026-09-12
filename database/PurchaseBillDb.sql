/*
=========================================================
Enhanzer Purchase Bill System
Database Setup Script
=========================================================
*/

-- Create database
IF NOT EXISTS (
    SELECT name
    FROM sys.databases
    WHERE name = 'PurchaseBillDb'
)
BEGIN
    CREATE DATABASE PurchaseBillDb;
END
GO

USE PurchaseBillDb;
GO


/*
=========================================================
1. Location_Details
Stores locations returned by the Enhanzer login API.
=========================================================
*/

IF NOT EXISTS (
    SELECT *
    FROM sys.tables
    WHERE name = 'Location_Details'
)
BEGIN
    CREATE TABLE Location_Details
    (
        Id INT IDENTITY(1,1) NOT NULL,
        Location_Code NVARCHAR(100) NOT NULL,
        Location_Name NVARCHAR(200) NOT NULL,

        CONSTRAINT PK_Location_Details
            PRIMARY KEY (Id)
    );
END
GO


/*
=========================================================
2. Purchase_Bill_Items
Stores items added to the Purchase Bill.
=========================================================
*/

IF NOT EXISTS (
    SELECT *
    FROM sys.tables
    WHERE name = 'Purchase_Bill_Items'
)
BEGIN
    CREATE TABLE Purchase_Bill_Items
    (
        Id INT IDENTITY(1,1) NOT NULL,

        Item NVARCHAR(100) NOT NULL,

        Batch NVARCHAR(200) NOT NULL,

        StandardCost DECIMAL(18,2) NOT NULL,

        StandardPrice DECIMAL(18,2) NOT NULL,

        Quantity INT NOT NULL,

        Discount DECIMAL(18,2) NOT NULL,

        TotalCost DECIMAL(18,2) NOT NULL,

        TotalSelling DECIMAL(18,2) NOT NULL,

        CONSTRAINT PK_Purchase_Bill_Items
            PRIMARY KEY (Id)
    );
END
GO


/*
=========================================================
Validation constraints
=========================================================
*/

IF NOT EXISTS (
    SELECT *
    FROM sys.check_constraints
    WHERE name = 'CK_Purchase_Bill_Items_Quantity'
)
BEGIN
    ALTER TABLE Purchase_Bill_Items
    ADD CONSTRAINT CK_Purchase_Bill_Items_Quantity
        CHECK (Quantity > 0);
END
GO


IF NOT EXISTS (
    SELECT *
    FROM sys.check_constraints
    WHERE name = 'CK_Purchase_Bill_Items_Discount'
)
BEGIN
    ALTER TABLE Purchase_Bill_Items
    ADD CONSTRAINT CK_Purchase_Bill_Items_Discount
        CHECK (Discount >= 0 AND Discount <= 100);
END
GO


IF NOT EXISTS (
    SELECT *
    FROM sys.check_constraints
    WHERE name = 'CK_Purchase_Bill_Items_StandardCost'
)
BEGIN
    ALTER TABLE Purchase_Bill_Items
    ADD CONSTRAINT CK_Purchase_Bill_Items_StandardCost
        CHECK (StandardCost >= 0);
END
GO


IF NOT EXISTS (
    SELECT *
    FROM sys.check_constraints
    WHERE name = 'CK_Purchase_Bill_Items_StandardPrice'
)
BEGIN
    ALTER TABLE Purchase_Bill_Items
    ADD CONSTRAINT CK_Purchase_Bill_Items_StandardPrice
        CHECK (StandardPrice >= 0);
END
GO


/*
=========================================================
Verify database structure
=========================================================
*/

SELECT
    TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;
GO