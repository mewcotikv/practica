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

-- Create Obiectiv Table
CREATE TABLE Obiectiv (
    IdObiectiv INT PRIMARY KEY IDENTITY(1,1),
    Denumire NVARCHAR(150) NOT NULL,
    IdClient INT NOT NULL,
    SuprafataM2 DECIMAL(10, 2) NOT NULL,
    Descriere NVARCHAR(200) NULL,
    Adresa NVARCHAR(100) NULL,
    Localitate NVARCHAR(50) NULL,
    DataCrearii DATETIME NOT NULL DEFAULT GETDATE(),
    DataFinalizarii DATETIME NULL,
    Status NVARCHAR(20) NULL DEFAULT 'Activ',
    CONSTRAINT FK_Obiectiv_Client FOREIGN KEY (IdClient) REFERENCES Client(IdClient)
);

-- Create Material Table
CREATE TABLE Material (
    IdMaterial INT PRIMARY KEY IDENTITY(1,1),
    Denumire NVARCHAR(100) NOT NULL,
    Tip NVARCHAR(50) NOT NULL,
    Descriere NVARCHAR(200) NULL,
    Pret DECIMAL(10, 2) NOT NULL,
    Unitate NVARCHAR(20) NOT NULL,
    DensitateKgM3 DECIMAL(10, 4) NOT NULL,
    ConductivitateTermica DECIMAL(10, 6) NOT NULL,
    StocDisponibil INT NOT NULL DEFAULT 0,
    DataAdaugarii DATETIME NOT NULL DEFAULT GETDATE(),
    Activ BIT NOT NULL DEFAULT 1
);

-- Create indexes for performance
CREATE INDEX IX_Obiectiv_IdClient ON Obiectiv(IdClient);
CREATE INDEX IX_Obiectiv_Status ON Obiectiv(Status);
CREATE INDEX IX_Material_Tip ON Material(Tip);
