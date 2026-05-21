# Calculator Materiale Termoizolatie
## RED Construct - Thermal Insulation Materials Calculator

### Project Overview

Calculator Materiale Termoizolatie is a WPF desktop application built with .NET 8 for RED Construct. It enables efficient calculation of thermal insulation material consumption for construction projects, client management, and order generation.

### Key Features

✅ **Client Management** - Add, edit, search and manage construction company clients  
✅ **Project Management** - Track building projects with surface areas and specifications  
✅ **Material Library** - Maintain database of thermal insulation materials with properties  
✅ **Consumption Calculation** - Automatic calculation of material quantities needed per project  
✅ **Order Generation** - Create detailed orders with pricing and discounts  
✅ **Reporting** - Generate comprehensive reports on orders, consumption, and revenue  
✅ **PDF Export** - Export calculations and orders as professional PDF documents  

---

## Project Structure

```
CalculatorMateriale/
│
├── Models/                          # Data Entity Classes
│   ├── Client.cs                   # Client information
│   ├── Obiectiv.cs                 # Construction projects
│   ├── Material.cs                 # Thermal insulation materials
│   ├── CalculConsum.cs             # Consumption calculations
│   ├── Comanda.cs                  # Orders
│   └── DetaliiComanda.cs           # Order line items
│
├── Data/                           # Data Access Layer
│   ├── ApplicationDbContext.cs     # EF Core DbContext
│   ├── Repository.cs               # Generic Repository pattern
│   └── UnitOfWork.cs               # Unit of Work pattern
│
├── Views/                          # WPF XAML Files (To be created)
│   ├── MainWindow.xaml
│   ├── ClientWindow.xaml
│   ├── ObiectivWindow.xaml
│   ├── MaterialWindow.xaml
│   ├── CalculConsumsWindow.xaml
│   └── ComandaWindow.xaml
│
├── ViewModels/                     # MVVM ViewModel Classes
│   ├── ClientViewModel.cs
│   ├── MaterialViewModel.cs
│   ├── ComandaViewModel.cs
│   └── MainViewModel.cs (To be created)
│
├── Helpers/                        # Utility Classes
│   ├── RelayCommand.cs             # ICommand implementation
│   ├── ViewModelBase.cs            # Base ViewModel class
│   └── MaterialCalculator.cs       # Calculation logic
│
├── Resources/                      # Styles & Templates
│   ├── Styles.xaml                 # Global styles
│   └── Converters.xaml             # Value converters
│
├── Documentation/                  # Project Documentation
│   ├── REQUIREMENTS.md             # Functional & non-functional requirements
│   ├── ERD_Diagram.md              # Entity Relationship Diagram (Mermaid)
│   ├── UseCase_Diagram.puml        # Use Case Diagram (PlantUML)
│   └── PROJECT_STRUCTURE.md        # This file
│
├── App.xaml                        # Application entry point
├── App.xaml.cs
└── CalculatorMateriale.csproj     # Project file (.NET 8)
```

---

## Technology Stack

| Component | Technology | Version |
|-----------|-----------|---------|
| **Framework** | .NET | 8.0+ |
| **UI Framework** | WPF | Built-in |
| **ORM** | Entity Framework Core | 8.0+ |
| **Database** | SQL Server / LocalDB | 2019+ |
| **Architecture** | MVVM | N/A |
| **Language** | C# | 12+ |

---

## Entity Relationships

```
CLIENT (1) ──→ (M) OBIECTIV
CLIENT (1) ──→ (M) COMANDA
OBIECTIV (1) ──→ (M) CALCUL_CONSUM
OBIECTIV (1) ──→ (M) COMANDA
MATERIAL (1) ──→ (M) CALCUL_CONSUM
MATERIAL (1) ──→ (M) DETALII_COMANDA
COMANDA (1) ──→ (M) DETALII_COMANDA
```

---

## Core Entities

### 1. **CLIENT** - Construction Company/Client
- Nume (Name)
- CUI (Tax ID)
- Adresa (Address)
- Telefon (Phone)
- Email
- Localitate (City)
- CodPostal (Postal Code)
- DataInregistrare (Registration Date)

### 2. **OBIECTIV** - Construction Project
- Denumire (Name)
- IdClient (Reference to Client)
- SuprafataM2 (Surface Area in m²)
- Descriere (Description)
- Adresa (Address)
- Localitate (City)
- Status (Active/Closed/Suspended)
- DataCrearii (Creation Date)
- DataFinalizarii (Completion Date)

### 3. **MATERIAL** - Thermal Insulation Material
- Denumire (Name)
- Tip (Type: Polistiren, Adeziv, Dibluri, Plasa, Tencuiala, Amorsa)
- Descriere (Description)
- Pret (Unit Price)
- Unitate (Unit: buc, kg, l, m², etc.)
- DensitateKgM3 (Density)
- ConductivitateTermica (Thermal Conductivity W/m·K)
- StocDisponibil (Available Stock)
- Activ (Active)

### 4. **CALCUL_CONSUM** - Material Consumption Calculation
- IdObiectiv (Project Reference)
- IdMaterial (Material Reference)
- ConsumPeM2 (Consumption per m²)
- ConsumTotal (Total Consumption = SuprafataM2 × ConsumPeM2)
- PretUnitar (Unit Price)
- PretTotal (Total Price)
- Grosime (Thickness in mm - for polystyrene)
- Randament (Application Efficiency %)
- DataCalcul (Calculation Date)
- Observatii (Notes)

### 5. **COMANDA** - Order
- IdClient (Client Reference)
- IdObiectiv (Project Reference - optional)
- DataComanda (Order Date)
- DataLivrare (Delivery Date)
- Status (New/Confirmed/Preparing/Delivered/Cancelled)
- ValoareTotala (Total Value)
- TVA (VAT Amount)
- Reducere (Discount)
- Observatii (Notes)

### 6. **DETALII_COMANDA** - Order Line Item
- IdComanda (Order Reference)
- IdMaterial (Material Reference)
- Cantitate (Quantity)
- PretUnitar (Unit Price)
- PretTotal (Line Total)
- ProcentReducere (Line Discount %)

---

## Getting Started

### Prerequisites
- Visual Studio 2022 or JetBrains Rider
- .NET 8 SDK
- SQL Server 2019+ or SQL Server Express LocalDB

### Setup Instructions

1. **Clone/Open Project**
   ```bash
   cd d:\redconstruct practica\CalculatorMateriale
   ```

2. **Restore NuGet Packages**
   ```bash
   dotnet restore
   ```

3. **Create Database**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. **Build Solution**
   ```bash
   dotnet build
   ```

5. **Run Application**
   ```bash
   dotnet run
   ```

---

## Key Classes & Responsibilities

### Models/
- **Client.cs** - Represents a client company
- **Obiectiv.cs** - Represents a construction project
- **Material.cs** - Represents a thermal insulation material
- **CalculConsum.cs** - Stores material consumption calculations
- **Comanda.cs** - Represents a sales order
- **DetaliiComanda.cs** - Order line items

### Data/
- **ApplicationDbContext.cs** - EF Core DbContext with Fluent API configuration
- **Repository<T>** - Generic repository for CRUD operations
- **UnitOfWork** - Coordinates multiple repositories

### ViewModels/
- **ClientViewModel** - Manages client CRUD operations
- **MaterialViewModel** - Manages material CRUD operations
- **ComandaViewModel** - Manages order CRUD operations

### Helpers/
- **RelayCommand** - Implements ICommand for button actions
- **ViewModelBase** - Provides INotifyPropertyChanged implementation
- **MaterialCalculator** - Contains calculation logic for material consumption

---

## Features Roadmap

### Phase 1 (Current) ✅
- Project structure setup
- Database design and EF Core models
- Basic CRUD operations for entities
- Material calculator logic

### Phase 2
- WPF UI implementation
- Client, Project, Material management windows
- Calculation interface
- Order management and printing

### Phase 3
- PDF export functionality
- Report generation
- Advanced search and filtering
- Data validation and error handling

### Phase 4
- User authentication & authorization
- Role-based access control
- Audit logging
- Application settings management

---

## Code Standards

- **Naming:** PascalCase for classes/methods, camelCase for fields
- **Architecture:** MVVM - strict separation of concerns
- **Database Access:** Async/await with Entity Framework Core
- **UI Binding:** XAML data binding with INotifyPropertyChanged
- **Commands:** Implement ICommand via RelayCommand
- **Documentation:** XML comments on public members

---

## Configuration

### Connection String
Edit `appsettings.json` to configure database:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CalculatorMateriale;Trusted_Connection=true;"
  }
}
```

### Default Materials
The database is seeded with 7 default thermal insulation materials:
- Expanded Polystyrene XPS 100mm & 80mm
- Polystyrene Adhesive
- Plastic Dowels
- Fiberglass Mesh
- Mineral Plaster
- Adhesion Primer

---

## Contact & Support

**Company:** RED Construct  
**Application:** Calculator Materiale Termoizolatie  
**Version:** 1.0  
**Last Updated:** May 21, 2026

---

## Documentation Files

| Document | Description | Location |
|----------|-------------|----------|
| **REQUIREMENTS.md** | Complete functional & non-functional requirements | Documentation/ |
| **ERD_Diagram.md** | Entity Relationship Diagram (Mermaid) | Documentation/ |
| **UseCase_Diagram.puml** | Use Case Diagram (PlantUML) | Documentation/ |
| **PROJECT_STRUCTURE.md** | Project structure overview | Documentation/ |

---

## License

Internal use only - RED Construct
