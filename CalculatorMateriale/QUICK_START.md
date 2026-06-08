# 🚀 QUICK START GUIDE

## Pentru a porni aplicația imediat:

### **Pas 1: Deschideți Terminalul PowerShell**
```powershell
cd "D:\redconstruct practica\CalculatorMateriale"
```

### **Pas 2: Rulați aplicația**
```powershell
dotnet run --project CalculatorMateriale.csproj
```

### **Pas 3: Aplicația se va deschide automat** 🎉

---

## 📋 INTERFAȚĂ PRINCIPALĂ

Meniu din stânga:
- 📊 **Dashboard** - Pagina de start
- 👥 **Clienți** - Gestionare clienți (CRUD)
- 📦 **Materiale** - Gestionare materiale (CRUD)
- 📋 **Comenzi** - Lista comenzi cu status
- 🧮 **Calcule** - **CALCULATOR MATERIALE** (START AICI!)
- 🏗️ **Proiecte** - Gestionare proiecte
- 📈 **Rapoarte** - **GENERATOR DEVIZ** (cu Export PDF)
- 📉 **Stocuri** - Monitorizare stocuri

---

## 🧮 UTILIZARE CALCULATOR MATERIALE

1. Click pe **🧮 Calcule** din meniu
2. Introduceți:
   - **Suprafață**: ex. 100 m²
   - **Tip Material**: alege din dropdown (Polistiren, Dibluri, etc.)
   - **Preț Unitar**: ex. 150 MDL
3. Click **📊 CALCULEAZĂ**
4. Vezi rezultatul cu TVA automat

**Exemplu rapid:**
```
Suprafață: 100 m²
Material: Polistiren
Preț: 185 MDL

→ Rezultat: 24,420 MDL (cu TVA 20%)
```

---

## 📄 UTILIZARE GENERATOR DEVIZ

1. Click pe **📈 Rapoarte** din meniu (aceasta deschide Deviz)
2. Completează:
   - Cliente: nume client
   - Proiect: denumire proiect
   - Data: auto-completat (azi)
3. Adaugă materiale:
   - Scrie material (ex. "Polistiren 100mm")
   - Suprafață (ex. 50)
   - Preț unitar (ex. 185)
   - Click **➕ Adaugă**
4. Click **📊 Calculeaza** - vei vedea totalul cu manoperă și TVA
5. Click **📄 Export PDF** - se salvează pe Desktop!

---

## 💡 FORMULE CALCUL (automatice)

```
✓ Polistiren = Suprafață × 1.10
✓ Dibluri = Suprafață × 6
✓ Adeziv = Suprafață ÷ 6
✓ Plasa = Suprafață × 1.15
✓ Tencuiala = Suprafață ÷ 4
✓ Amorsa = Suprafață ÷ 10
✓ Manoperă = +35% din materiale
✓ TVA = +20% (total final)
```

---

## 📊 MANAGEMENT COMENZI

1. Click **📋 Comenzi**
2. Vizualizare toate comenzile în tabel
3. Filtru după status: Toate, Noua, Confirmata, Finalizata
4. Butoane:
   - **✏️ Editare** - modifică date
   - **🗑️ Ștergere** - șterge comandă (cu confirmare)
   - **📊 Raport** - generează raport

---

## 👥 MANAGEMENT CLIENȚI

1. Click **👥 Clienți**
2. Vizualizare toți clienții
3. Căutare după nume/CUI
4. Butoane:
   - **➕ Adaugă** - client nou
   - **✏️ Editare** - modifică date
   - **🗑️ Ștergere** - șterge client

---

## 🐛 TROUBLESHOOTING

### Dacă nu pornește aplicația:

**Eroare: "Specify which project file..."**
```powershell
# Rulează exact asta:
dotnet run --project CalculatorMateriale.csproj -c Debug
```

**Eroare: "Bază de date nu se conectează"**
- Verifică dacă LocalDB este instalat: `sqlcmd -S (localdb)\mssqllocaldb`
- Dacă nu, instalează SQL Server Express LocalDB
- Verifică `appsettings.json` connection string

**Eroare: "Package QuestPDF"**
- Rulează: `dotnet restore`

---

## 📁 FIȘIERE IMPORTANTE

```
CalculatorMateriale.csproj          → Configurare proiect
appsettings.json                    → Connection string BD
Database/RedConstructDB_Script.sql  → Creare BD manual
Views/CalculatorView.xaml           → UI Calculator
Views/DevizView.xaml                → UI Generator Deviz
Helpers/MaterialCalculator.cs       → Formule calcul
```

---

## 🎯 CE SUNT DATELE TEST?

Baza de date vine preîncărcată cu:
- 5 clienți din Moldova (ABC Construction, Termosistem, etc.)
- 10 materiale termoizolante (Polistiren 100mm, Adeziv, Dibluri, etc.)
- Date gata pentru testare

Poți adăuga mai mult din UI!

---

## 📞 NOTES PENTRU PREZENTARE

✅ **Simplitate**: Interfață ușor de folosit
✅ **Formule corecte**: Specifice termoizolație
✅ **Professionalism**: Export PDF calitativ
✅ **Funcțional**: Toate modulele active
✅ **Database**: Conectare automată LocalDB
✅ **Design**: Modern, responsive, user-friendly

**Perfect pentru practica la RED Construct!** 🎓
