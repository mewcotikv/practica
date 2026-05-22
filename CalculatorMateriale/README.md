# Calculator Materiale Termoizolație - Ghid de Pornire Rapidă

## 📋 Prezentare
Aceasta este o aplicație desktop profesională WPF .NET 8 pentru RED Construct care calculează necesarul de materiale de termoizolație și gestionează comenzile proiectelor de construcție.

## 🏗️ Structura Proiectului

```
CalculatorMateriale/
├── Models/          → Clase entitate (Client, Material, Comandă, etc.)
├── Data/            → Context bază de date, depozite, unitate de lucru
├── Views/           → Fișiere XAML (UI WPF - de creat)
├── ViewModels/      → Logică MVVM și comenzi
├── Helpers/         → Utilitare, calcule, convertoare
├── Resources/       → Stiluri, șabloane, resurse
└── Documentation/   → Cerințe, diagrame, arhitectură
```

## ✨ Caracteristici Principale

- **Gestionarea Clienților** - Înregistrare și gestionare a companiilor de construcții
- **Urmărirea Proiectelor** - Urmărire proiecte cu suprafețe în m²
- **Biblioteca de Materiale** - Gestionare materiale termoizolație (polistiren, adeziv, dibleuri, plase, tencuieli, apretură)
- **Calcule Consum** - Calcul automat al cantităților de materiale
- **Gestionarea Comenzilor** - Creare, modificare și urmărire comenzi cu prețuri
- **Export PDF** - Generare rapoarte și facturi profesionale
- **Raportări** - Rapoarte vânzări, analiză consum materiale, urmărire venituri

## 🗄️ Entități Principale

| Entitate | Scop |
|----------|------|
| **CLIENT** | Companii/clienți de construcții |
| **OBIECTIV** | Proiecte de construcție (proprietăți cu suprafață în m²) |
| **MATERIAL** | Materiale de termoizolație cu proprietăți termice |
| **CALCUL_CONSUM** | Calcule consum materiale per proiect |
| **COMANDA** | Comenzi de vânzare |
| **DETALII_COMANDA** | Linii articole din comandă |

## 📊 Schema Bazei de Date

**Relații între Entități:**
```
CLIENT (1) ──→ (M) OBIECTIV
CLIENT (1) ──→ (M) COMANDA
OBIECTIV (1) ──→ (M) CALCUL_CONSUM
MATERIAL (1) ──→ (M) CALCUL_CONSUM
MATERIAL (1) ──→ (M) DETALII_COMANDA
COMANDA (1) ──→ (M) DETALII_COMANDA
```

Vezi [ERD_Diagram.md](Documentation/ERD_Diagram.md) pentru structura detaliată a entităților.

## 🚀 Primii Pași

### Condiții Prealabile
- Visual Studio 2022+ sau Rider
- .NET 8 SDK
- SQL Server 2019+ sau LocalDB

### Pași Configurare

1. **Restaurează pachete NuGet:**
   ```bash
   dotnet restore
   ```

2. **Creează și migrează baza de date:**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

3. **Construiește soluția:**
   ```bash
   dotnet build
   ```

4. **Rulează aplicația:**
   ```bash
   dotnet run
   ```

## 🎯 Pași Următori

1. **Implementează Views** - Creează ferestre XAML pentru fiecare modul
   - MainWindow.xaml
   - ClientWindow.xaml
   - ObiectivWindow.xaml
   - MaterialWindow.xaml
   - CalculConsumWindow.xaml
   - ComandaWindow.xaml

2. **Completează ViewModels** - Implementează clasele ViewModels rămase
   - MainViewModel
   - ObiectivViewModel
   - CalculConsumViewModel

3. **Adaugă Export PDF** - Implementează generare PDF pentru rapoarte
4. **Interfață Utilizator** - Stil cu principii Material Design
5. **Testare** - Teste unitare și de integrare

## 📖 Fișiere Documentație

| Fișier | Conținut |
|--------|----------|
| [REQUIREMENTS.md](Documentation/REQUIREMENTS.md) | Cerințe funcționale și non-funcționale complete |
| [ERD_Diagram.md](Documentation/ERD_Diagram.md) | Diagrama Relații Entități (Mermaid) |
| [UseCase_Diagram.puml](Documentation/UseCase_Diagram.puml) | Diagrama Cazuri de Utilizare (PlantUML) |
| [PROJECT_STRUCTURE.md](Documentation/PROJECT_STRUCTURE.md) | Explicație detaliată structură proiect |

## 🔧 Stiva Tehnologică

- **Framework:** .NET 8
- **UI:** WPF (Windows Presentation Foundation)
- **ORM:** Entity Framework Core 8
- **Bază de Date:** SQL Server / LocalDB
- **Arhitectură:** MVVM (Model-View-ViewModel)
- **Limbă:** C# 12

## 🏢 Companie

**RED Construct** - Specialist Termoizolație  
Versiunea Aplicație: 1.0  
Creat: 21 mai 2026

---

Pentru cerințe detaliate, vezi [REQUIREMENTS.md](Documentation/REQUIREMENTS.md)
