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
    CONSTRAINT FK_Obiectiv_Client FOREIGN KEY (IdClient) 
        REFERENCES Client(IdClient) 
        ON DELETE CASCADE 
        ON UPDATE CASCADE
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

-- Create CalculConsum Table
CREATE TABLE CalculConsum (
    IdCalculConsum INT PRIMARY KEY IDENTITY(1,1),
    IdObiectiv INT NOT NULL,
    IdMaterial INT NOT NULL,
    ConsumPeM2 DECIMAL(10, 4) NOT NULL,
    ConsumTotal DECIMAL(10, 4) NOT NULL,
    PretUnitar DECIMAL(10, 2) NOT NULL,
    PretTotal DECIMAL(10, 2) NULL,
    Grosime INT NULL,
    Randament DECIMAL(5, 2) NULL,
    DataCalcul DATETIME NOT NULL DEFAULT GETDATE(),
    Observatii NVARCHAR(200) NULL,
    CONSTRAINT FK_CalculConsum_Obiectiv FOREIGN KEY (IdObiectiv) 
        REFERENCES Obiectiv(IdObiectiv) 
        ON DELETE CASCADE 
        ON UPDATE CASCADE,
    CONSTRAINT FK_CalculConsum_Material FOREIGN KEY (IdMaterial) 
        REFERENCES Material(IdMaterial) 
        ON DELETE RESTRICT 
        ON UPDATE CASCADE
);

-- Create Comanda Table
CREATE TABLE Comanda (
    IdComanda INT PRIMARY KEY IDENTITY(1,1),
    IdClient INT NOT NULL,
    IdObiectiv INT NULL,
    DataComanda DATETIME NOT NULL DEFAULT GETDATE(),
    DataLivrare DATETIME NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Noua',
    ValoareTotala DECIMAL(10, 2) NULL,
    TVA DECIMAL(10, 2) NULL,
    Reducere DECIMAL(10, 2) NULL,
    Observatii NVARCHAR(300) NULL,
    CONSTRAINT FK_Comanda_Client FOREIGN KEY (IdClient) 
        REFERENCES Client(IdClient) 
        ON DELETE CASCADE 
        ON UPDATE CASCADE,
    CONSTRAINT FK_Comanda_Obiectiv FOREIGN KEY (IdObiectiv) 
        REFERENCES Obiectiv(IdObiectiv) 
        ON DELETE SET NULL 
        ON UPDATE CASCADE
);

-- Create DetaliiComanda Table
CREATE TABLE DetaliiComanda (
    IdDetaliiComanda INT PRIMARY KEY IDENTITY(1,1),
    IdComanda INT NOT NULL,
    IdMaterial INT NOT NULL,
    Cantitate DECIMAL(10, 4) NOT NULL,
    PretUnitar DECIMAL(10, 2) NOT NULL,
    PretTotal DECIMAL(10, 2) NULL,
    ProcentReducere DECIMAL(5, 2) NULL,
    CONSTRAINT FK_DetaliiComanda_Comanda FOREIGN KEY (IdComanda) 
        REFERENCES Comanda(IdComanda) 
        ON DELETE CASCADE 
        ON UPDATE CASCADE,
    CONSTRAINT FK_DetaliiComanda_Material FOREIGN KEY (IdMaterial) 
        REFERENCES Material(IdMaterial) 
        ON DELETE RESTRICT 
        ON UPDATE CASCADE
);

-- Create indexes for the new tables
CREATE INDEX IX_CalculConsum_IdObiectiv ON CalculConsum(IdObiectiv);
CREATE INDEX IX_CalculConsum_IdMaterial ON CalculConsum(IdMaterial);
CREATE INDEX IX_Comanda_IdClient ON Comanda(IdClient);
CREATE INDEX IX_Comanda_IdObiectiv ON Comanda(IdObiectiv);
CREATE INDEX IX_Comanda_Status ON Comanda(Status);
CREATE INDEX IX_DetaliiComanda_IdComanda ON DetaliiComanda(IdComanda);
CREATE INDEX IX_DetaliiComanda_IdMaterial ON DetaliiComanda(IdMaterial);

-- ============================================
-- INSERT DATE DE TEST - CLIENȚI DIN MOLDOVA
-- ============================================
INSERT INTO Client (Nume, CUI, Adresa, Telefon, Email, Localitate, CodPostal) VALUES
('SC ABC Construction SRL', 'MD12345678', 'Str. Ștefan cel Mare nr. 45', '+373 22 123 456', 'contact@abcconstruction.md', 'Chișinău', 'MD2012'),
('OOO Constructii Moldova', 'MD87654321', 'Bulevardul Dacia nr. 78', '+373 22 234 567', 'info@constructiimd.md', 'Chișinău', 'MD2001'),
('SRL Proiectare și Construcții', 'MD11223344', 'Piața Marii Adunări Naționale nr. 1', '+373 22 345 678', 'proiecte@proiectare.md', 'Chișinău', 'MD2012'),
('SC Termosistem SRL', 'MD55667788', 'Str. Tiraspol nr. 102', '+373 22 456 789', 'termosistem@gmail.md', 'Bălți', 'MD3100'),
('OOO Reparații Locuințe', 'MD99887766', 'Str. Puteștei nr. 56', '+373 22 567 890', 'reparatii@locuinte.md', 'Chișinău', 'MD2005');

-- ============================================
-- INSERT DATE DE TEST - 10 MATERIALE TERMOIZOLAȚIE
-- ============================================
INSERT INTO Material (Denumire, Tip, Descriere, Pret, Unitate, DensitateKgM3, ConductivitateTermica, StocDisponibil, Activ) VALUES
('Polistiren Expandat 100mm', 'Polistiren', 'Plăci de polistiren expandat densitate 25 kg/m³, 100mm grosime', 185.00, 'mp', 25.0, 0.035, 500, 1),
('Polistiren Expandat 150mm', 'Polistiren', 'Plăci de polistiren expandat densitate 25 kg/m³, 150mm grosime', 250.00, 'mp', 25.0, 0.035, 350, 1),
('Polistiren Extrudat 80mm', 'Polistiren', 'Plăci de polistiren extrudat, 80mm grosime, cu barier de vapori', 320.00, 'mp', 35.0, 0.032, 200, 1),
('Vată Minerală 100mm', 'Lână minerală', 'Saltea de vată minerală, 100mm grosime, densitate 50 kg/m³', 140.00, 'mp', 50.0, 0.040, 400, 1),
('Adeziv pentru Polistiren', 'Adeziv', 'Adeziv poliuretanic pentru fixare plăci polistiren pe zidărie', 75.00, 'kg', 1200.0, 0.240, 800, 1),
('Dibluri Plastice cu Șurub', 'Dibluri', 'Dibluri din plastic cu șurub metalic pentru fixare polistiren', 1.50, 'buc', 900.0, 0.250, 5000, 1),
('Plasă de Sticlă Armată', 'Plasă', 'Plasă din fibră de sticlă pentru întărire tencuielii, 160 g/m²', 28.00, 'mp', 2500.0, 0.500, 600, 1),
('Tencuială de Bază Minerală', 'Tencuiala', 'Tencuială minerală pentru bază, pe bază de ciment și var, 25kg/sac', 130.00, 'sac', 1800.0, 0.800, 300, 1),
('Amorsa (Grund) Dispersie', 'Amorsa', 'Amorsa de adezivitate pe bază de dispersie, 1L', 95.00, 'l', 1100.0, 0.260, 400, 1),
('Tencuială Finală Acrilică', 'Tencuiala', 'Tencuială finală pe bază de răşini acrilice, 25kg/sac', 180.00, 'sac', 1700.0, 0.900, 250, 1);
