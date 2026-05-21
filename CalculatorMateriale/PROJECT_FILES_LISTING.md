# Calculator Materiale Termoizolatie - Project Structure Overview

## 📁 Complete Project File Listing

```
CalculatorMateriale/
│
├── 📋 MODELS LAYER (6 Entity Classes)
│   ├── Models/Client.cs
│   │   └── [IdClient, Nume, CUI, Adresa, Telefon, Email, Localitate, CodPostal, DataInregistrare]
│   │
│   ├── Models/Obiectiv.cs
│   │   └── [IdObiectiv, Denumire, IdClient, SuprafataM2, Descriere, Adresa, Status, Dates]
│   │
│   ├── Models/Material.cs
│   │   └── [IdMaterial, Denumire, Tip, Descriere, Pret, Unitate, DensitateKgM3, ConductivitateTermica, Stoc, Activ]
│   │
│   ├── Models/CalculConsum.cs
│   │   └── [IdCalculConsum, IdObiectiv, IdMaterial, ConsumPeM2, ConsumTotal, PretTotal, Grosime, Randament]
│   │
│   ├── Models/Comanda.cs
│   │   └── [IdComanda, IdClient, IdObiectiv, DataComanda, Status, ValoareTotala, TVA, Reducere]
│   │
│   └── Models/DetaliiComanda.cs
│       └── [IdDetaliiComanda, IdComanda, IdMaterial, Cantitate, PretUnitar, PretTotal]
│
├── 🗄️ DATA ACCESS LAYER (3 Files)
│   ├── Data/ApplicationDbContext.cs
│   │   └── [DbContext, DbSets for all 6 entities, Fluent API configuration, Relationships, Seed data]
│   │
│   ├── Data/Repository.cs
│   │   └── [IRepository<T>, Repository<T> - Generic CRUD operations, Async support]
│   │
│   └── Data/UnitOfWork.cs
│       └── [IUnitOfWork, UnitOfWork - Coordinates repositories, SaveChangesAsync]
│
├── 📱 VIEW MODELS LAYER (5 ViewModels)
│   ├── ViewModels/ClientViewModel.cs
│   │   └── [LoadClientiCommand, AddClientCommand, EditClientCommand, DeleteClientCommand, SearchCommand]
│   │
│   ├── ViewModels/ObiectivViewModel.cs
│   │   └── [LoadObiectiveCommand, AddObiectivCommand, EditObiectivCommand, DeleteObiectivCommand, FilterByClientCommand]
│   │
│   ├── ViewModels/MaterialViewModel.cs
│   │   └── [LoadMaterialeCommand, AddMaterialCommand, EditMaterialCommand, DeleteMaterialCommand]
│   │
│   ├── ViewModels/CalculConsumViewModel.cs
│   │   └── [LoadCalculConsume, AddCalculCommand, EditCalculCommand, DeleteCalculCommand, FilterByProjectCommand, ExportPDFCommand]
│   │
│   └── ViewModels/ComandaViewModel.cs
│       └── [LoadComenziCommand, AddComandaCommand, EditComandaCommand, DeleteComandaCommand, PrintComandaCommand]
│
├── 🛠️ HELPERS/UTILITIES (3 Classes)
│   ├── Helpers/RelayCommand.cs
│   │   └── [RelayCommand, RelayCommand<T> - ICommand implementation]
│   │
│   ├── Helpers/ViewModelBase.cs
│   │   └── [ViewModelBase - INotifyPropertyChanged, SetProperty helper]
│   │
│   └── Helpers/MaterialCalculator.cs
│       └── [CalculateConsumTotal, CalculatePretTotal, CalculateAdezivNecesar, CalculateDibluriNecesari, etc.]
│
├── 🎨 VIEWS (Directory - Ready for XAML)
│   ├── Views/MainWindow.xaml (To be created)
│   ├── Views/ClientWindow.xaml (To be created)
│   ├── Views/ObiectivWindow.xaml (To be created)
│   ├── Views/MaterialWindow.xaml (To be created)
│   ├── Views/CalculConsumWindow.xaml (To be created)
│   └── Views/ComandaWindow.xaml (To be created)
│
├── 🎯 RESOURCES (Directory - Ready for Styles)
│   ├── Resources/Styles.xaml (To be created)
│   ├── Resources/Colors.xaml (To be created)
│   └── Resources/Templates.xaml (To be created)
│
├── 📚 DOCUMENTATION (4 Files)
│   ├── Documentation/REQUIREMENTS.md
│   │   └── [400+ lines covering FR1-FR7, NFR1-NFR7, Database schema, Business rules]
│   │
│   ├── Documentation/ERD_Diagram.md
│   │   └── [Mermaid ERD with all 6 entities, fields, relationships, keys]
│   │
│   ├── Documentation/UseCase_Diagram.puml
│   │   └── [PlantUML Use Case diagram with 32+ use cases, 3 actors]
│   │
│   └── Documentation/PROJECT_STRUCTURE.md
│       └── [Detailed project structure explanation, tech stack, setup guide]
│
├── ⚙️ CONFIGURATION FILES
│   ├── CalculatorMateriale.csproj
│   │   └── [.NET 8 project file, NuGet dependencies, Assembly info, Compile settings]
│   │
│   ├── appsettings.json
│   │   └── [Production configuration, DefaultConnection, VAT 19%, RON currency]
│   │
│   └── appsettings.Development.json
│       └── [Development configuration, Debug connection, Debug logging]
│
├── 📖 ROOT DOCUMENTATION
│   ├── README.md
│   │   └── [Quick start guide, features overview, tech stack, setup instructions]
│   │
│   ├── CHANGELOG.md
│   │   └── [Version 1.0.0 release notes, Phase 2-4 roadmap]
│   │
│   ├── DEVELOPMENT.md
│   │   └── [Architecture decisions, standards, implementation guidelines, troubleshooting]
│   │
│   ├── DELIVERY_SUMMARY.md
│   │   └── [Complete project delivery checklist and summary]
│   │
│   └── PROJECT_FILES_LISTING.md
│       └── [This file - Complete file listing and overview]
│
└── 🌟 READY FOR (Next Phases)
    ├── Phase 2: UI Implementation (XAML)
    ├── Phase 3: PDF Export & Reporting
    ├── Phase 4: Auth & Settings
    └── Production Deployment
```

---

## 📊 Statistics

| Category | Count |
|----------|-------|
| **Entity Models** | 6 |
| **Repository Classes** | 1 Generic |
| **ViewModels** | 5 |
| **Helper Classes** | 3 |
| **Documentation Files** | 7 |
| **Configuration Files** | 3 |
| **C# Classes Total** | 17 |
| **Total Lines of Code** | 2,500+ |
| **Documentation Lines** | 2,000+ |
| **NuGet Packages** | 13 |

---

## 🎯 Database Entities Summary

### 1. CLIENT (Clienti)
```csharp
┌─ IdClient (PK)
├─ Nume (100 chars)
├─ CUI (50 chars, UNIQUE)
├─ Adresa (100 chars)
├─ Telefon (20 chars)
├─ Email (100 chars)
├─ Localitate (50 chars)
├─ CodPostal (10 chars)
└─ DataInregistrare (DateTime)
```

### 2. OBIECTIV (Obiective)
```csharp
┌─ IdObiectiv (PK)
├─ Denumire (150 chars)
├─ IdClient (FK) ──→ CLIENT
├─ SuprafataM2 (Decimal, >0)
├─ Descriere (200 chars)
├─ Adresa (100 chars)
├─ Localitate (50 chars)
├─ DataCrearii (DateTime)
├─ DataFinalizarii (DateTime, nullable)
└─ Status (Active/Closed/Suspended)
```

### 3. MATERIAL (Materiale)
```csharp
┌─ IdMaterial (PK)
├─ Denumire (100 chars)
├─ Tip (Polistiren|Adeziv|Dibluri|Plasa|Tencuiala|Amorsa)
├─ Descriere (200 chars)
├─ Pret (Decimal)
├─ Unitate (buc|kg|l|mp|etc)
├─ DensitateKgM3 (Decimal)
├─ ConductivitateTermica (W/m·K)
├─ StocDisponibil (Integer)
├─ DataAdaugarii (DateTime)
└─ Activ (Boolean)
```

### 4. CALCUL_CONSUM (CalculConsume)
```csharp
┌─ IdCalculConsum (PK)
├─ IdObiectiv (FK) ──→ OBIECTIV
├─ IdMaterial (FK) ──→ MATERIAL
├─ ConsumPeM2 (Decimal)
├─ ConsumTotal (Decimal)
├─ PretUnitar (Decimal)
├─ PretTotal (Decimal)
├─ Grosime (Integer, nullable) [mm]
├─ Randament (Decimal, nullable) [%]
├─ DataCalcul (DateTime)
└─ Observatii (200 chars)
```

### 5. COMANDA (Comenzi)
```csharp
┌─ IdComanda (PK)
├─ IdClient (FK) ──→ CLIENT
├─ IdObiectiv (FK, nullable) ──→ OBIECTIV
├─ DataComanda (DateTime)
├─ DataLivrare (DateTime, nullable)
├─ Status (Noua|Confirmata|In Preparare|Livrata|Anulata)
├─ ValoareTotala (Decimal)
├─ TVA (Decimal, nullable)
├─ Reducere (Decimal, nullable)
└─ Observatii (300 chars)
```

### 6. DETALII_COMANDA (DetaliiComenzi)
```csharp
┌─ IdDetaliiComanda (PK)
├─ IdComanda (FK) ──→ COMANDA
├─ IdMaterial (FK) ──→ MATERIAL
├─ Cantitate (Decimal)
├─ PretUnitar (Decimal)
├─ PretTotal (Decimal)
└─ ProcentReducere (Decimal, nullable)
```

---

## 🔄 Entity Relationships Map

```
┌─────────────────────────────────────────────────────────────┐
│                        CLIENT                               │
│                    (Clienti Table)                          │
└────────────┬────────────────────────────────────┬───────────┘
             │ (1)                            (1) │
             │ to (M)                         to (M)
             ▼                                   ▼
     ┌──────────────┐                    ┌──────────────┐
     │  OBIECTIV    │                    │   COMANDA    │
     │ (Obiective)  │                    │  (Comenzi)   │
     └──────┬───────┘                    └──────────────┘
            │ (1)                              │ (1)
            │ to (M)                          │ to (M)
            ├─────────────┬────────────┐      │
            ▼             ▼            │      │
    ┌──────────────┐  ┌──────────────┐│      │
    │CALCUL_CONSUM │  │   COMANDA    ││      │
    │(CalculC.)   │  │  (Comenzi)   ││      │
    └──────┬───────┘  └──────┬───────┘│      │
           │                 │        │      │
           │ (1)            │ (1)    │      │
           │ to (M)         │ to (M) │      │
           │ ┌──────────────┼────────┼──────┴──────┐
           │ │              │        │             │
           ▼ ▼              ▼        │             ▼
    ┌──────────────┐  ┌──────────────────┐  ┌────────────┐
    │  MATERIAL    │  │ DETALII_COMANDA  │  │ OBIECTIV   │
    │(Materiale)   │  │ (DetaliiComenzi) │  │(Obiective) │
    └──────────────┘  └──────────────────┘  └────────────┘
```

---

## 🚀 Implementation Checklist

### ✅ Completed (Phase 1)
- [x] Project structure setup
- [x] Entity models (6 classes)
- [x] Database context configuration
- [x] Repository pattern implementation
- [x] Unit of Work pattern
- [x] ViewModel layer (5 classes)
- [x] Helper utilities
- [x] NuGet dependencies
- [x] Configuration files
- [x] Documentation (7 files)

### ⏳ Next - Phase 2
- [ ] XAML UI files (6 windows)
- [ ] MainViewModel
- [ ] Styling and resources
- [ ] Data binding implementation
- [ ] UI state management

### 🔮 Future - Phase 3
- [ ] PDF export functionality
- [ ] Report generation
- [ ] Print functionality
- [ ] Data import/export

### 🌟 Future - Phase 4
- [ ] User authentication
- [ ] Role-based access control
- [ ] Audit logging
- [ ] Settings management

---

## 🎨 Material Types Catalog

| # | Type | Unit | Purpose | Consumption |
|----|------|------|---------|------------|
| 1 | Polistiren | buc | Thermal insulation boards | 1 piece/m² |
| 2 | Adeziv | sac (25kg) | Adhesive for mounting | 5 kg/m² |
| 3 | Dibluri | buc | Mechanical anchors | 4 pieces/m² |
| 4 | Plasa | m² | Mesh reinforcement | 1.1 m²/m² |
| 5 | Tencuiala | sac (25kg) | Base/finish plaster | 7 kg/m² |
| 6 | Amorsa | galeata (10l) | Adhesion primer | 0.5 l/m² |

---

## 📈 Project Metrics

```
Architecture Compliance:    100% MVVM
Code Organization:         Well-structured
Documentation:             Comprehensive (7 files)
Entity Coverage:           6 entities, all relationships modeled
Business Logic:            Calculator utilities included
Data Access:               Repository + Unit of Work pattern
Async Support:             Throughout data layer
Configuration:             Development & Production
NuGet Packages:            13 (latest versions)
Database Ready:            Yes (LocalDB configured)
```

---

## 🔐 Security Features Planned

- Windows Authentication
- Role-based access control
- Audit logging
- Data encryption
- Input validation
- SQL injection prevention (EF Core)

---

## 📱 User Roles

1. **Admin** - Full system access, user management, settings
2. **Operator** - CRUD operations, calculations, orders, reports
3. **Viewer** - View-only access to data and reports

---

## 🌐 Localization Settings

- **Language:** Romanian (ro-RO)
- **Date Format:** dd/MM/yyyy
- **Currency:** RON (Romanian Lei)
- **Decimal Separator:** , (comma)
- **Timezone:** UTC+2/UTC+3

---

## 📞 Project Contact

**Company:** RED Construct  
**Product:** Calculator Materiale Termoizolatie  
**Version:** 1.0.0  
**Framework:** .NET 8  
**Pattern:** MVVM  
**Status:** Development Ready  

---

**Last Generated:** May 21, 2026
