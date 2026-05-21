# Calculator Materiale Termoizolatie - Quick Start Guide

## 📋 Overview
This is a professional WPF .NET 8 desktop application for RED Construct that calculates thermal insulation material requirements and manages construction project orders.

## 🏗️ Project Structure

```
CalculatorMateriale/
├── Models/          → Entity classes (Client, Material, Order, etc.)
├── Data/            → Database context, repositories, unit of work
├── Views/           → XAML files (WPF UI - to be created)
├── ViewModels/      → MVVM logic and commands
├── Helpers/         → Utilities, calculators, converters
├── Resources/       → Styles, templates, assets
└── Documentation/   → Requirements, diagrams, architecture
```

## ✨ Key Features

- **Client Management** - Register and manage construction companies
- **Project Tracking** - Track building projects with surface areas
- **Material Library** - Manage thermal insulation materials (polystyrene, adhesive, dowels, mesh, plaster, primer)
- **Consumption Calculations** - Automatic calculation of material quantities
- **Order Management** - Create, modify, and track orders with pricing
- **PDF Export** - Generate professional reports and invoices
- **Reporting** - Sales reports, material consumption analysis, revenue tracking

## 🗄️ Core Entities

| Entity | Purpose |
|--------|---------|
| **CLIENT** | Construction companies/clients |
| **OBIECTIV** | Building projects (properties with m² surface) |
| **MATERIAL** | Thermal insulation materials with thermal properties |
| **CALCUL_CONSUM** | Material consumption calculations per project |
| **COMANDA** | Sales orders |
| **DETALII_COMANDA** | Order line items |

## 📊 Database Schema

**Entity Relationships:**
```
CLIENT (1) ──→ (M) OBIECTIV
CLIENT (1) ──→ (M) COMANDA
OBIECTIV (1) ──→ (M) CALCUL_CONSUM
MATERIAL (1) ──→ (M) CALCUL_CONSUM
MATERIAL (1) ──→ (M) DETALII_COMANDA
COMANDA (1) ──→ (M) DETALII_COMANDA
```

See [ERD_Diagram.md](Documentation/ERD_Diagram.md) for detailed entity structure.

## 🚀 Getting Started

### Prerequisites
- Visual Studio 2022+ or Rider
- .NET 8 SDK
- SQL Server 2019+ or LocalDB

### Setup Steps

1. **Restore NuGet packages:**
   ```bash
   dotnet restore
   ```

2. **Create and migrate database:**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

3. **Build the solution:**
   ```bash
   dotnet build
   ```

4. **Run the application:**
   ```bash
   dotnet run
   ```

## 🎯 Next Steps

1. **Implement Views** - Create XAML windows for each module
   - MainWindow.xaml
   - ClientWindow.xaml
   - ObiectivWindow.xaml
   - MaterialWindow.xaml
   - CalculConsumWindow.xaml
   - ComandaWindow.xaml

2. **Complete ViewModels** - Implement remaining ViewModel classes
   - MainViewModel
   - ObiectivViewModel
   - CalculConsumViewModel

3. **Add PDF Export** - Implement PDF generation for reports
4. **User Interface** - Style with Material Design principles
5. **Testing** - Unit and integration tests

## 📖 Documentation Files

| File | Content |
|------|---------|
| [REQUIREMENTS.md](Documentation/REQUIREMENTS.md) | Complete functional & non-functional requirements |
| [ERD_Diagram.md](Documentation/ERD_Diagram.md) | Entity Relationship Diagram (Mermaid) |
| [UseCase_Diagram.puml](Documentation/UseCase_Diagram.puml) | Use Case Diagram (PlantUML) |
| [PROJECT_STRUCTURE.md](Documentation/PROJECT_STRUCTURE.md) | Detailed project structure explanation |

## 🔧 Technology Stack

- **Framework:** .NET 8
- **UI:** WPF (Windows Presentation Foundation)
- **ORM:** Entity Framework Core 8
- **Database:** SQL Server / LocalDB
- **Architecture:** MVVM (Model-View-ViewModel)
- **Language:** C# 12

## 🏢 Company

**RED Construct** - Thermal Insulation Specialist  
Application Version: 1.0  
Created: May 21, 2026

---

For detailed requirements, see [REQUIREMENTS.md](Documentation/REQUIREMENTS.md)
