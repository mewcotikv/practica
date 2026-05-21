# 🎯 Calculator Materiale Termoizolatie - Complete Project Index

## Welcome to the RED Construct Thermal Insulation Materials Calculator

This is a professional-grade WPF .NET 8 desktop application ready for development. All project structure, entity models, data access layer, and comprehensive documentation are included.

---

## 📑 Quick Navigation

### 🏗️ Architecture & Structure
- [Project Structure Overview](PROJECT_FILES_LISTING.md) - Complete file tree and statistics
- [Project Structure Documentation](Documentation/PROJECT_STRUCTURE.md) - Detailed explanations
- [README](README.md) - Quick start guide

### 📋 Documentation & Specifications
- [Requirements Document](Documentation/REQUIREMENTS.md) ⭐ **START HERE** - 400+ lines of complete specifications
  - Functional Requirements (FR-1 through FR-7)
  - Non-Functional Requirements (NFR-1 through NFR-7)
  - Database schema
  - Business rules
  - User roles
  
### 🎨 Diagrams
- [ERD Diagram](Documentation/ERD_Diagram.md) - Entity Relationship Diagram (Mermaid format)
- [Use Case Diagram](Documentation/UseCase_Diagram.puml) - Use case diagram (PlantUML format)

### 💻 Code & Implementation
- [Development Notes](DEVELOPMENT.md) - Architecture decisions, standards, guidelines
- [Changelog](CHANGELOG.md) - Version history and roadmap

### 📊 Project Summary
- [Delivery Summary](DELIVERY_SUMMARY.md) - Complete delivery checklist

---

## 🚀 Quick Start

### 1. **Open Project**
```bash
cd d:\redconstruct practica\CalculatorMateriale
```

### 2. **Restore Dependencies**
```bash
dotnet restore
```

### 3. **Create Database**
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. **Build & Run**
```bash
dotnet build
dotnet run
```

---

## 📁 Project Structure

```
CalculatorMateriale/
├── Models/              (6 Entity Classes)
├── Data/                (Database & Repository Layer)
├── ViewModels/          (5 MVVM ViewModels)
├── Helpers/             (Utilities & Calculators)
├── Views/               (Ready for XAML - Phase 2)
├── Resources/           (Ready for Styles - Phase 2)
├── Documentation/       (Complete specs)
└── Configuration Files  (.csproj, appsettings.json)
```

---

## 🎯 Key Components

### Models (6 Entity Classes)
1. **Client** - Client/Company information
2. **Obiectiv** - Building projects
3. **Material** - Thermal insulation materials
4. **CalculConsum** - Material consumption calculations
5. **Comanda** - Orders
6. **DetaliiComanda** - Order line items

### Data Access Layer
- **ApplicationDbContext** - EF Core configuration
- **Repository** - Generic CRUD repository
- **UnitOfWork** - Coordinates repositories

### ViewModels (5 Classes)
- **ClientViewModel** - Client management
- **ObiectivViewModel** - Project management
- **MaterialViewModel** - Material management
- **CalculConsumViewModel** - Calculations
- **ComandaViewModel** - Order management

### Helpers
- **RelayCommand** - ICommand implementation
- **ViewModelBase** - MVVM base class
- **MaterialCalculator** - Calculation utilities

---

## 🔗 Entity Relationships

```
CLIENT (1) ──→ (M) OBIECTIV
CLIENT (1) ──→ (M) COMANDA
OBIECTIV (1) ──→ (M) CALCUL_CONSUM
MATERIAL (1) ──→ (M) CALCUL_CONSUM
MATERIAL (1) ──→ (M) DETALII_COMANDA
COMANDA (1) ──→ (M) DETALII_COMANDA
```

---

## 📊 Material Types Supported

1. **Polistiren** - Expanded Polystyrene (XPS 100mm, 80mm)
2. **Adeziv** - Adhesive
3. **Dibluri** - Plastic Dowels
4. **Plasa** - Fiberglass Mesh
5. **Tencuiala** - Mineral Plaster
6. **Amorsa** - Adhesion Primer

Each material includes:
- Unit price
- Unit type (buc, kg, l, m², sac, galeata)
- Density (kg/m³)
- Thermal conductivity (W/m·K)
- Stock availability
- Custom properties

---

## 🛠️ Technology Stack

| Technology | Version |
|-----------|---------|
| .NET Framework | 8.0 |
| C# Language | 12+ |
| UI Framework | WPF |
| ORM | Entity Framework Core 8 |
| Database | SQL Server / LocalDB 2019+ |
| Architecture | MVVM |

---

## 📖 How to Use This Documentation

### For Project Managers
→ Start with [REQUIREMENTS.md](Documentation/REQUIREMENTS.md) for complete feature list

### For Developers
→ Start with [DEVELOPMENT.md](DEVELOPMENT.md) for architecture and standards

### For Business Analysts
→ Start with [Use Case Diagram](Documentation/UseCase_Diagram.puml) and [ERD Diagram](Documentation/ERD_Diagram.md)

### For Database Architects
→ Check [ERD_Diagram.md](Documentation/ERD_Diagram.md) for complete schema

### For DevOps/Infrastructure
→ Review [CalculatorMateriale.csproj](CalculatorMateriale.csproj) and [appsettings.json](appsettings.json)

---

## ✅ Deliverables Checklist

- ✅ Full MVVM project structure
- ✅ 6 Entity models with relationships
- ✅ Data access layer (Context, Repository, UnitOfWork)
- ✅ 5 Complete ViewModels with CRUD
- ✅ Helper utilities and calculators
- ✅ Project configuration (.csproj)
- ✅ Database configuration (appsettings.json)
- ✅ Requirements document (400+ lines)
- ✅ ERD diagram (Mermaid)
- ✅ Use case diagram (PlantUML)
- ✅ Development guidelines
- ✅ Quick start guide
- ✅ 7+ documentation files

---

## 🎓 Documentation Files

| File | Type | Content |
|------|------|---------|
| [REQUIREMENTS.md](Documentation/REQUIREMENTS.md) | Specification | Complete functional & non-functional requirements |
| [ERD_Diagram.md](Documentation/ERD_Diagram.md) | Technical | Entity relationship diagram with all fields |
| [UseCase_Diagram.puml](Documentation/UseCase_Diagram.puml) | Technical | 32+ use cases for all features |
| [PROJECT_STRUCTURE.md](Documentation/PROJECT_STRUCTURE.md) | Guide | Detailed structure explanation |
| [README.md](README.md) | Guide | Quick start and overview |
| [DEVELOPMENT.md](DEVELOPMENT.md) | Reference | Architecture decisions and standards |
| [CHANGELOG.md](CHANGELOG.md) | Reference | Version history and roadmap |
| [DELIVERY_SUMMARY.md](DELIVERY_SUMMARY.md) | Report | Project delivery checklist |
| [PROJECT_FILES_LISTING.md](PROJECT_FILES_LISTING.md) | Index | Complete file listing |
| [INDEX.md](INDEX.md) | Navigation | This file |

---

## 🚀 Next Phases

### Phase 2: UI Development
- XAML files for MainWindow and 6 management windows
- Styling and Material Design
- Data binding implementation
- MainViewModel

### Phase 3: Advanced Features
- PDF export using iText7
- Reporting module with charts
- Print functionality
- Data import/export

### Phase 4: Enterprise Features
- User authentication
- Role-based access control
- Audit logging
- Settings management

---

## 🔍 Key Features

✨ **Client Management** - Register and manage construction companies  
✨ **Project Tracking** - Track building projects with surface areas  
✨ **Material Library** - Manage 6 types of thermal materials  
✨ **Consumption Calculation** - Automatic material quantity calculations  
✨ **Order Management** - Create and track orders with pricing  
✨ **PDF Export** - Generate professional reports  
✨ **Reporting** - Sales, consumption, and revenue analysis  

---

## 💼 Company Information

**Company:** RED Construct  
**Product:** Calculator Materiale Termoizolatie  
**Application Type:** WPF Desktop Application  
**Framework:** .NET 8  
**Architecture:** MVVM  
**Database:** SQL Server / LocalDB  
**Status:** Development Ready  
**Version:** 1.0.0  
**Created:** May 21, 2026  

---

## 📞 Support

For questions about:
- **Architecture** → See [DEVELOPMENT.md](DEVELOPMENT.md)
- **Requirements** → See [REQUIREMENTS.md](Documentation/REQUIREMENTS.md)
- **Setup** → See [README.md](README.md)
- **Database** → See [ERD_Diagram.md](Documentation/ERD_Diagram.md)
- **Use Cases** → See [UseCase_Diagram.puml](Documentation/UseCase_Diagram.puml)

---

## 🎉 You're All Set!

The project is ready for development. All groundwork has been completed:

✓ Architecture designed  
✓ Database schema modeled  
✓ Business logic layer ready  
✓ Configuration set up  
✓ Comprehensive documentation provided  

**Next Step:** Begin UI/XAML implementation or review the requirements with your team.

---

**Index Last Updated:** May 21, 2026  
**Project Version:** 1.0.0  
**Framework:** .NET 8  
**Architecture:** MVVM
