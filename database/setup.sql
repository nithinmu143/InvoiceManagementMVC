IF DB_ID(N'invoices') IS NULL
BEGIN
    CREATE DATABASE [invoices];
END
GO

USE [invoices];
GO

IF OBJECT_ID(N'dbo.Invoices', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Invoices
    (
        InvoiceNumber int IDENTITY(1,1) NOT NULL,
        DateOfIssue datetime NOT NULL,

        CONSTRAINT PK_Invoices
            PRIMARY KEY (InvoiceNumber)
    );
END
GO

IF OBJECT_ID(N'dbo.InvoiceItem', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.InvoiceItem
    (
        ID int IDENTITY(1,1) NOT NULL,
        InvoiceNumber int NOT NULL,
        Title nvarchar(255) NULL,
        Quantity decimal(10,2) NULL,
        Price decimal(10,2) NULL,

        CONSTRAINT PK_InvoiceItem
            PRIMARY KEY (ID),

        CONSTRAINT FK_InvoiceItem_Invoices
            FOREIGN KEY (InvoiceNumber)
            REFERENCES dbo.Invoices(InvoiceNumber)
    );
END
GO