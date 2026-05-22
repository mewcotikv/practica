# Note Dezvoltare

## Prezentare Proiect
**Calculator Materiale Termoizolație** - Calculator Materiale Termoizolație RED Construct
- **Platformă:** WPF .NET 8
- **Tipar:** MVVM
- **Bază de Date:** SQL Server / LocalDB
- **Creat:** 21 mai 2026

---

## Decizii Arhitectură

### 1. Tipar MVVM
**De ce:** Separă UI de logică afaceri, permite testabilitate și suport designer
- **Modele:** Entități date cu validare
- **Vizualizări:** UI XAML (WPF)
- **ViewModels:** Logică afaceri și comenzi
- **Date:** Depozite și servicii

### 2. Entity Framework Core
**De ce:** ORM modern pentru .NET, suportă LINQ, migrări și operații async
- Tipar generic depozit pentru reutilizare cod
- Tipar unitate lucru pentru coordonare tranzacții
- API Fluent pentru configurare
- Proprietăți shadow pentru câmpuri audit

### 3. Async/Await
**De ce:** UI responsiv, operații bază date eficiente
- Toate apelurile bază date sunt async
- Operații lungi se execută pe thread pool
- Firul UI rămâne responsiv

### 4. Injecție Dependență
**De ce:** Cuplare slabă, testabilitate, întreținere
- Container gestionat de Microsoft.Extensions.DependencyInjection
- Configurare în App.xaml.cs
- Servicii injectate în ViewModels

---

## Tipuri Materiale & Calcule

### Materiale Suportate
1. **Polistiren (Polistiren Expandat)**
   - Tipuri: XPS 100mm, 80mm
   - Unitate: buc (bucată)
   - Densitate: 35 kg/m³
   - Conductivitate termică: 0.029 W/m·K
   - Consum: 1 bucată/m² (100mm = ~4.5 kg/m²)

2. **Adeziv (Adeziv)**
   - Unitate: sac (sac - 25kg)
   - Consum: 5 kg/m² (~0.2 saci/m²)

3. **Dibleuri (Dibleuri)**
   - Unitate: buc (bucată)
   - Consum: 4 bucăți/m²

4. **Plasă (Plasă)**
   - Unitate: mp (m²)
   - Consum: 1.1 m²/m² (10% suprapunere)

5. **Tencuiala (Tencuiala)**
   - Unitate: sac (sac - 25kg)
   - Consum: ~7 kg/m² (~0.28 saci/m²)

6. **Amorsa (Apretură)**
   - Unitate: galeată (galeată - 10l)
   - Consum: 0.5 l/m²

---

## Design Bază de Date

### Entități
- **Client:** Stochează informații client/companie
- **Obiectiv:** Proiecte construcție cu suprafață (m²)
- **Material:** Proprietăți termice, prețuri
- **CalculConsum:** Calcule consum per material per proiect
- **Comanda:** Comenzi cu urmărire status
- **DetaliiComanda:** Articole linie comandă

### Relații
- Un client → Multe proiecte (1:M)
- Un proiect → Multe calcule (1:M)
- Un proiect → Multe comenzi (1:M)
- Un material → Multe calcule (1:M)
- O comandă → Multe articole linie (1:M)

### Comportament Ștergere
- Ștergere client restricționată dacă are proiecte
- Ștergere proiect cascadă calcule
- Ștergere material restricționată dacă folosit în calcule
- Ștergere comandă cascadă articole linie

---

## Implementare Caracteristici Cheie

### 1. Calcul Consum Materiale
```csharp
ConsumTotal = SuprafataM2 × ConsumPeM2
PretTotal = ConsumTotal × PretUnitar
```

### 2. Calcul TVA
```csharp
PretCuTVA = PretTotal × (1 + TVA/100)  // Implicit 19%
```

### 3. Total Comandă
```csharp
ValoareTotala = SUM(DetaliiComanda.PretTotal) + TVA - Reducere
```

---

## Arhitectură ViewModels

### Clasă Bază: ViewModelBase
- Implementează INotifyPropertyChanged
- Oferă ajutor SetProperty pentru legare bidirecțională
- Metoda OnPropertyChanged pentru notificări

### RelayCommand
- Implementare ICommand
- Versiuni generice și non-generice
- Suportă predicat CanExecute

### ViewModels Specifice
1. **ClientViewModel**
   - LoadClientiCommand
   - AddClientCommand
   - EditClientCommand
   - DeleteClientCommand
   - SearchCommand

2. **MaterialViewModel**
   - LoadMaterialeCommand
   - AddMaterialCommand
   - EditMaterialCommand
   - DeleteMaterialCommand

3. **ComandaViewModel**
   - LoadComenziCommand
   - AddComandaCommand
   - EditComandaCommand
   - DeleteComandaCommand
   - PrintComandaCommand

---

## Strat Acces Date

### Tipar Depozit
- Interfață generică `IRepository<T>`
- Implementare `Repository<T>`
- Operații CRUD standard
- Suport async/await

### Unitate Lucru
- Coordonează mai multe depozite
- Gestionează domeniu tranzacție
- Apel unic SaveChanges()
- Asigură consistență date

---

## Configurare

### Șir Conexiune
```
Server=(localdb)\mssqllocaldb;Database=CalculatorMateriale;Trusted_Connection=true;
```

### Setări Aplicație
- TVA Implicit: 19%
- Monedă: RON (Lei Români)
- Format Dată: dd/MM/yyyy
- Separator Zecimal: , (virgulă)

---

## Standarde Cod

### Convenții Denumire
- Clase: PascalCase (ex: `ClientViewModel`)
- Metode: PascalCase (ex: `LoadClienti()`)
- Proprietăți: PascalCase (ex: `SelectedClient`)
- Câmpuri: camelCase cu prefix underscore (ex: `_clienti`)
- Constante: UPPER_CASE (ex: `DEFAULT_VAT`)

### Documentație XML
- Membrii publici trebuie să aibă comentarii XML
- Includ rezumat, remarci, exemple unde este cazul
- Folosesc taguri `<param>`, `<returns>`, `<exception>`

### Organizare Fișiere
- O clasă per fișier (cu excepția tipurilor imbricate)
