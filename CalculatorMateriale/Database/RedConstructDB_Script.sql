-- Create RedConstructDB Database
CREATE DATABASE RedConstructDB;

-- Use the database
USE RedConstructDB;

-- Create Client Table
CREATE TABLE Client (
    IdClient INT PRIMARY KEY IDENTITY(1,1),
    Nume NVARCHAR(100) NOT NULL,
    CUI NVARCHAR(50) NOT NULL,
    Adresa NVARCHAR(100) NULL,
    Telefon NVARCHAR(20) NULL,
    Email NVARCHAR(100) NULL,
    Localitate NVARCHAR(50) NULL,
    CodPostal NVARCHAR(10) NULL,
    DataInregistrare DATETIME NOT NULL DEFAULT GETDATE()
);

-- Create index on CUI for faster lookups
CREATE UNIQUE INDEX IX_Client_CUI ON Client(CUI);
