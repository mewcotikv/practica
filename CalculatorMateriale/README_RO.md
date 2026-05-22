# 🏗️ Calculator Materiale Termoizolație - RED Construct

## Ce se întâmplă în acest proiect?

Aceasta este o **aplicație desktop profesională** construită în **C# .NET 8 cu WPF** care ajută firma RED Construct să:

- **Gestioneze clienții** - Stochează informații despre companiile partenere din Moldova
- **Administreze proiecte de construcție** (Obiective) - Urmărește lucrările la case, clădiri, etc.
- **Gestioneze materiale de termoizolație** - Polistiren, vată minerală, adezivi, dibleuri, plase, tencuieli
- **Calculeze consumul de materiale** - Determină cantitatea exactă de material necesară pentru fiecare proiect
- **Genereze și urmărească comenzi** - Creează comenzi cu prețuri și detalii
- **Factureze clienții** - Calculează totalul cu TVA și reduceri

---

## 🎯 De ce există acest proiect?

RED Construct trebuie să calculeze precis cantitatea de materiale de izolație termică pentru fiecare proiect. Fără o aplicație, calculele se fac manual în Excel, iar aceasta este **lentă**, **predispusă la erori** și **greu de urmărit**.

**Soluția:** O aplicație care automatizează totul!

---

## 📊 Cum funcționează?

### Fluxul de lucru:

1. **Adaugă un client** (o firmă de construcții sau proprietar)
   - Nume, CUI, adresa, telefon, email

2. **Creează un proiect** (Obiectiv) pentru acel client
   - Denumire: ex. "Izolație termică Hotel Central"
   - Suprafață: ex. 1500 m²
   - Status: Activ, Închis, Suspendat

3. **Selectează materiale** și calculează consumul
   - Ex: Polistiren 100mm = 185 MDL/m²
   - Pentru 1500 m²: 1500 × 185 = 277,500 MDL

4. **Generează comanda** cu detalii
   - Ce materiale, cantitate, preț unitar
   - TVA și reduceri automate
   - Preț total final

5. **Urmărește comanda** (Nouă → Confirmată → În Pregătire → Livrată)

---

## 💾 Baza de date

Aplicația folosește **SQL Server** cu 6 tabele principale:

| Tabelă | Ce conține |
|--------|-----------|
| **Client** | Companiile partenere (5 clienți din Moldova în date test) |
| **Obiectiv** | Proiectele/lucrările la construcții |
| **Material** | Materiale de termoizolație (10 tipuri cu prețuri în MDL) |
| **CalculConsum** | Calculele de consum pentru fiecare proiect |
| **Comanda** | Comenzile plasate |
| **DetaliiComanda** | Liniile din fiecare comandă (ce materiale, câte, la ce preț) |

### Materiale din baza de date (test):
- ✅ Polistiren Expandat 100mm - 185 MDL/m²
- ✅ Polistiren Expandat 150mm - 250 MDL/m²
- ✅ Polistiren Extrudat 80mm - 320 MDL/m²
- ✅ Vată Minerală 100mm - 140 MDL/m²
- ✅ Adeziv pentru Polistiren - 75 MDL/kg
- ✅ Dibleuri Plastice - 1.50 MDL/buc
- ✅ Plasă de Sticlă Armată - 28 MDL/m²
- ✅ Tencuială de Bază - 130 MDL/sac
- ✅ Amorsa (Grund) - 95 MDL/litru
- ✅ Tencuială Finală Acrilică - 180 MDL/sac

### 5 Clienți test din Moldova:
1. SC ABC Construction SRL - Chișinău
2. OOO Constructii Moldova - Chișinău
3. SRL Proiectare și Construcții - Chișinău
4. SC Termosistem SRL - Bălți
5. OOO Reparații Locuințe - Chișinău

---

## 🛠️ Cum rulează?

### Tehnologia folosită:
- **Framework:** .NET 8
- **UI:** WPF (Windows Presentation Foundation)
- **Pattern:** MVVM (Model-View-ViewModel)
- **Database:** Entity Framework Core + SQL Server
- **Language:** C#

### Structura folderelor:
```
CalculatorMateriale/
├── Models/              → Entitățile bazei de date (Client, Obiectiv, Material, etc.)
├── Data/                → Conexiunea la baza de date și accesul la date
├── ViewModels/          → Logica de business pentru fiecare ecran
├── Helpers/             → Utilitare (calcule, comenzi refolosibile)
├── Views/               → Interfața grafică (XAML)
├── Resources/           → Stiluri și teme
├── Documentation/       → Specificații și diagrame
└── Database/            → Scriptul SQL pentru creare bază de date
```

---

## 🚀 Etapele proiectului

### ✅ Faza 1: COMPLETĂ (Baza de date)
- ✅ Toate modelele de date create
- ✅ Relații și chei externe configurate
- ✅ Reguli de integritate referențială (ON DELETE CASCADE, ON DELETE RESTRICT)
- ✅ Date de test adăugate (5 clienți, 10 materiale)
- ✅ Script SQL gata de rulare

### 🔄 Faza 2: ÎN CURS (Interfața grafică)
- 🔲 XAML Views pentru fiecare funcționalitate
- 🔲 Stiluri și teme profesionale
- 🔲 Validări de intrare
- 🔲 Mesaje de eroare user-friendly

### ⏳ Faza 3: TESTARE
- 🔲 Teste unitare
- 🔲 Teste de integrare
- 🔲 Rapoarte de performanță

---

## 📝 Ce e în GitHub?

Tot codul și scripturile SQL sunt deja trimise pe GitHub la:  
👉 **https://github.com/mewcotikv/practica**

Fiecare pas important are un **commit cu mesaj în română** care explică ce s-a schimbat:
- "Adaugă date de test: 5 clienți din Moldova și 10 materiale de termoizolație cu prețuri în MDL"
- "Adaugă cheile externe FOREIGN KEY cu reguli ON DELETE"
- etc.

---

## 💡 De reținut

- **Scopul principal:** Calcul rapid și precis al materiale de termoizolație
- **Beneficiar:** RED Construct (firma de construcții)
- **Utilizatori:** Angajații care gestionează proiecte, comenzi și materiale
- **Limbă:** Aplicația e în limba română (clienți din Moldova)
- **Monedă:** Prețuri în MDL (lei moldovenești)

---

*Proiect creat mai 2026 - RED Construct Materials Calculator*
