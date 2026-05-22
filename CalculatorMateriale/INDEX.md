# 🎯 Calculator Materiale Termoizolație - Index Complet Proiect

## Bun venit la Calculator Materiale Termoizolație RED Construct

Aceasta este o aplicație desktop profesională WPF .NET 8 gata pentru dezvoltare. Toată structura proiectului, modelele entitate, stratul acces date și documentația cuprinzătoare sunt incluse.

---

## 📑 Navigare Rapidă

### 🏗️ Arhitectură & Structură
- [Prezentare Structură Proiect](PROJECT_FILES_LISTING.md) - Arbore fișiere complet și statistici
- [Documentație Structură Proiect](Documentation/PROJECT_STRUCTURE.md) - Explicații detaliate
- [CITEȘTE-MĂ](README.md) - Ghid pornire rapidă

### 📋 Documentație & Specificații
- [Document Cerințe](Documentation/REQUIREMENTS.md) ⭐ **ÎNCEPE AICI** - 400+ linii de specificații complete
  - Cerințe Funcționale (FR-1 până FR-7)
  - Cerințe Non-Funcționale (NFR-1 până NFR-7)
  - Schema bază de date
  - Reguli afaceri
  - Roluri utilizator
  
### 🎨 Diagrame
- [Diagrama ERD](Documentation/ERD_Diagram.md) - Diagrama Relații Entități (format Mermaid)
- [Diagrama Cazuri Utilizare](Documentation/UseCase_Diagram.puml) - Diagrama cazuri utilizare (format PlantUML)

### 💻 Cod & Implementare
- [Note Dezvoltare](DEVELOPMENT.md) - Decizii arhitectură, standarde, linii directoare
- [Jurnal Schimbări](CHANGELOG.md) - Istoric versiuni și hartă traseu

### 📊 Rezumat Proiect
- [Rezumat Livrare](DELIVERY_SUMMARY.md) - Listă verificare livrare completă

---

## 🚀 Pornire Rapidă

### 1. **Deschide Proiectul**
```bash
cd d:\redconstruct practica\CalculatorMateriale
```

### 2. **Restaurează Dependențe**
```bash
dotnet restore
```

### 3. **Creează Baza de Date**
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. **Construiește & Rulează**
```bash
dotnet build
dotnet run
```

---

## 📁 Structura Proiect

```
CalculatorMateriale/
├── Models/              (6 Clase Entitate)
├── Data/                (Strat Bază de Date & Depozit)
├── ViewModels/          (5 ViewModels MVVM)
├── Helpers/             (Utilitare & Calcule)
├── Views/               (Gata pentru XAML - Faza 2)
├── Resources/           (Gata pentru Stiluri - Faza 2)
├── Documentation/       (Specificații complete)
└── Fișiere Configurare  (.csproj, appsettings.json)
```

---

## 🎯 Componente Cheie

### Modele (6 Clase Entitate)
1. **Client** - Informații client/companie
2. **Obiectiv** - Proiecte construcție
3. **Material** - Materiale termoizolație
4. **CalculConsum** - Calcule consum materiale
5. **Comanda** - Comenzi
6. **DetaliiComanda** - Articole linie comandă

### Strat Acces Date
- **ApplicationDbContext** - Configurare EF Core
- **Repository** - Depozit generic CRUD
- **UnitOfWork** - Coordonează depozite

### ViewModels (5 Clase)
- **ClientViewModel** - Gestionare clienți
- **ObiectivViewModel** - Gestionare proiecte
- **MaterialViewModel** - Gestionare materiale
- **CalculConsumViewModel** - Calcule
- **ComandaViewModel** - Gestionare comenzi


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
