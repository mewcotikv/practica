# Calculator Materiale Termoizolație
## RED Construct - Calculator Materiale Termoizolație

### Prezentare Proiect

Calculator Materiale Termoizolație este o aplicație desktop WPF construită cu .NET 8 pentru RED Construct. Permite calcul eficient al consumului de materiale de termoizolație pentru proiecte de construcție, gestionare clienți și generare comenzi.

### Caracteristici Cheie

✅ **Gestionare Clienți** - Adaugă, editează, caută și gestionează clienți companiilor de construcții  
✅ **Gestionare Proiecte** - Urmărește proiecte construcție cu suprafețe și specificații  
✅ **Bibliotecă Materiale** - Menține bază date materiale termoizolație cu proprietăți  
✅ **Calcul Consum** - Calcul automat cantități materiale necesare per proiect  
✅ **Generare Comenzi** - Creează comenzi detaliate cu prețuri și reduceri  
✅ **Raportări** - Generează rapoarte cuprinzătoare pe comenzi, consum și venituri  
✅ **Export PDF** - Exportă calcule și comenzi ca documente PDF profesionale  

---

## Structura Proiect

```
CalculatorMateriale/
│
├── Models/                          # Clase Entitate Date
│   ├── Client.cs                   # Informații client
│   ├── Obiectiv.cs                 # Proiecte construcție
│   ├── Material.cs                 # Materiale termoizolație
│   ├── CalculConsum.cs             # Calcule consum
│   ├── Comanda.cs                  # Comenzi
│   └── DetaliiComanda.cs           # Articole linie comandă
│
├── Data/                           # Strat Acces Date
│   ├── ApplicationDbContext.cs     # DbContext EF Core
│   ├── Repository.cs               # Tipar Depozit Generic
│   └── UnitOfWork.cs               # Tipar Unitate Lucru
│
├── Views/                          # Fișiere XAML WPF (De creat)
│   ├── MainWindow.xaml
│   ├── ClientWindow.xaml
│   ├── ObiectivWindow.xaml
│   ├── MaterialWindow.xaml
│   ├── CalculConsumsWindow.xaml
│   └── ComandaWindow.xaml
│
├── ViewModels/                     # Clase ViewModel MVVM
│   ├── ClientViewModel.cs
│   ├── MaterialViewModel.cs
│   ├── ComandaViewModel.cs
│   └── MainViewModel.cs (De creat)
│
├── Helpers/                        # Clase Utilitare
│   ├── RelayCommand.cs             # Implementare ICommand
│   ├── ViewModelBase.cs            # Clasă bază ViewModel
│   └── MaterialCalculator.cs       # Logică calcul
│
├── Resources/                      # Stiluri & Șabloane
│   ├── Styles.xaml                 # Stiluri globale
│   └── Converters.xaml             # Convertoare valori
│
├── Documentation/                  # Documentație Proiect
│   ├── REQUIREMENTS.md             # Cerințe funcționale & non-funcționale
│   ├── ERD_Diagram.md              # Diagrama Relații Entități (Mermaid)
│   ├── UseCase_Diagram.puml        # Diagrama Cazuri Utilizare (PlantUML)
│   └── PROJECT_STRUCTURE.md        # Acest fișier
│
├── App.xaml                        # Punct intrare aplicație
├── App.xaml.cs
└── CalculatorMateriale.csproj     # Fișier proiect (.NET 8)
```

---

## Stiva Tehnologică

| Componentă | Tehnologie | Versiune |
|-----------|-----------|----------|
| **Framework** | .NET | 8.0+ |
| **Framework UI** | WPF | Încorporat |
| **ORM** | Entity Framework Core | 8.0+ |
| **Bază de Date** | SQL Server / LocalDB | 2019+ |
| **Arhitectură** | MVVM | N/A |
| **Limbă** | C# | 12+ |

---

## Relații Entități

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

## Entități Principale

### 1. **CLIENT** - Companie Construcție/Client
- Nume (Nume)
- CUI (CUI)
- Adresa (Adresă)
- Telefon (Telefon)
- Email
- Localitate (Localitate)
- CodPostal (Cod Poștal)
- DataInregistrare (Data Înregistrare)

### 2. **OBIECTIV** - Proiect Construcție
- Denumire (Denumire)
- IdClient (Referință Client)
- SuprafataM2 (Suprafață în m²)
- Descriere (Descriere)
- Adresa (Adresă)
- Localitate (Localitate)
- Status (Activ/Închis/Suspendat)
- DataCrearii (Data Creare)
- DataFinalizarii (Data Finalizare)

### 3. **MATERIAL** - Material Termoizolație
- Denumire (Denumire)
- Tip (Tip: Polistiren, Adeziv, Dibleuri, Plasă, Tencuiala, Amorsa)
- Descriere (Descriere)
- Pret (Preț Unitar)
- Unitate (Unitate: buc, kg, l, m², etc.)
- DensitateKgM3 (Densitate)
- ConductivitateTermica (Conductivitate Termică W/m·K)
- StocDisponibil (Stoc Disponibil)
- Activ (Activ)

### 4. **CALCUL_CONSUM** - Calcul Consum Material
- IdObiectiv (Referință Proiect)
- IdMaterial (Referință Material)
- ConsumPeM2 (Consum per m²)
- ConsumTotal (Consum Total = SuprafataM2 × ConsumPeM2)
- PretUnitar (Preț Unitar)
- PretTotal (Preț Total)
- Grosime (Grosime în mm - pentru polistiren)
- Randament (Eficiență Aplicare %)
- DataCalcul (Data Calcul)
- Observatii (Note)

### 5. **COMANDA** - Comandă
- IdClient (Referință Client)
- IdObiectiv (Referință Proiect - opțional)
- DataComanda (Data Comandă)
- DataLivrare (Data Livrare)
- Status (Nouă/Confirmată/Pregătire/Livrată/Anulată)
- ValoareTotala (Valoare Totală)
- TVA (Sumă TVA)
- Reducere (Reducere)
- Observatii (Note)

### 6. **DETALII_COMANDA** - Articol Linie Comandă
- IdComanda (Referință Comandă)
- IdMaterial (Referință Material)
- Cantitate (Cantitate)
- PretUnitar (Preț Unitar)
- PretTotal (Total Linie)
- ProcentReducere (Reducere % Linie)

---

## Pornire Rapidă

### Condiții Prealabile
- Visual Studio 2022 sau JetBrains Rider
- .NET 8 SDK
- SQL Server 2019+ sau LocalDB SQL Server Express

### Instrucțiuni Configurare

1. **Clonează/Deschide Proiectul**
   ```bash
   cd d:\redconstruct practica\CalculatorMateriale
   ```

2. **Restaurează Pachete NuGet**
   ```bash
   dotnet restore
   ```

3. **Creează Baza de Date**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. **Construiește Soluția**
   ```bash
   dotnet build
   ```

5. **Rulează Aplicația**
   ```bash
   dotnet run
   ```

---

## Clase Cheie & Responsabilități

### Models/
- **Client.cs** - Reprezintă o companie client
- **Obiectiv.cs** - Reprezintă un proiect construcție
- **Material.cs** - Reprezintă un material termoizolație
- **CalculConsum.cs** - Stochează calcule consum materiale
- **Comanda.cs** - Reprezintă o comandă vânzare
- **DetaliiComanda.cs** - Articole linie comandă

### Data/
- **ApplicationDbContext.cs** - DbContext EF Core cu configurare API Fluent
- **Repository<T>** - Depozit generic pentru operații CRUD
- **UnitOfWork** - Coordonează mai multe depozite

### ViewModels/
- **ClientViewModel** - Gestionează operații CRUD clienți
- **MaterialViewModel** - Gestionează operații CRUD materiale
- **ComandaViewModel** - Gestionează operații CRUD comenzi

### Helpers/
- **RelayCommand** - Implementează ICommand pentru acțiuni buton
- **ViewModelBase** - Oferă implementare INotifyPropertyChanged
- **MaterialCalculator** - Conține logică calcul consum materiale

---

## Hartă Traseu Caracteristici

### Faza 1 (Curent) ✅
- Configurare structură proiect
- Design bază de date și modele EF Core
- Operații CRUD de bază pentru entități
- Logică calculator materiale

### Faza 2
- Implementare UI WPF
- Ferestre gestionare Client, Proiect, Material
- Interfață calcul
- Gestionare comenzi și tipărire

### Faza 3
- Funcționalitate export PDF
- Generare rapoarte
- Căutare și filtrare avansate
- Validare date și gestionare erori

### Faza 4
- Autentificare & autorizare utilizator
- Control acces bazat pe roluri
- Jurnalizare audit
- Gestionare setări aplicație

---

## Standarde Cod

- **Denumire:** PascalCase pentru clase/metode, camelCase pentru câmpuri
- **Arhitectură:** MVVM - separare strictă responsabilități
- **Acces Bază Date:** Async/await cu Entity Framework Core
- **Legare UI:** Legare date XAML cu INotifyPropertyChanged
- **Comenzi:** Implementare ICommand via RelayCommand
- **Documentație:** Comentarii XML pe membri publici

---

## Configurare

### Șir Conexiune
Editează `appsettings.json` pentru configurare bază de date:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CalculatorMateriale;Trusted_Connection=true;"
  }
}
```

### Materiale Implicite
Baza de date este populată cu 7 materiale termoizolație implicite:
- Polistiren Expandat XPS 100mm & 80mm
- Adeziv Polistiren
- Dibleuri Plastice
- Plasă Fibră Sticlă
- Tencuiala Minerală
- Apretură Aderență

---

## Contact & Suport

**Companie:** RED Construct  
**Aplicație:** Calculator Materiale Termoizolație  
**Versiune:** 1.0  
**Actualizat Ultima:** 21 mai 2026
