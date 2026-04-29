# 🎯 Cum Să Creezi Issues pe GitHub - Instrucțiuni

## ✅ OPȚIUNEA 1: Creare Automată cu GitHub CLI (Recomandat)

Dacă ai GitHub CLI instalat:

```bash
# 1. Autentificare
gh auth login

# 2. Navigare în folder
cd c:\Users\User\Desktop\practica\REDConstruct

# 3. Creare issues din lista
gh issue create --title "🐛 #1 - DataGrid nu se reîmprospătează" --body "Rezolvat ✅" --label "bug,resolved"
gh issue create --title "🐛 #2 - DatePicker validare" --body "Rezolvat ✅" --label "bug,resolved"
gh issue create --title "✨ #3 - Raport material" --body "Implementat ✅" --label "feature,resolved"
# ... etc
```

## ✅ OPȚIUNEA 2: Creare Manuală pe GitHub Web

Mergi pe: https://github.com/mewcotikv/practica/issues/new

Și creează una câte una cu informatiile de mai jos:

---

### 🐛 ERORI (Bugs)

#### Issue #1
- **Titlu:** 🐛 DataGrid nu se reîmprospătează după export
- **Body:**
```
**STATUS: ✅ REZOLVAT**

Problemă: DataGrid nu se reîmprospătează corect după export Excel.

Soluție implementată:
- Adaugă UI refresh forțat
- Error handling în RapoarteViewModel
- Validare date

Branch: issue/1-datagrid-refresh
```
- **Labels:** bug, resolved

#### Issue #2
- **Titlu:** 🐛 DatePicker nu validează Data Sfarsit < Data Inceput
- **Body:**
```
**STATUS: ✅ REZOLVAT**

Problemă: Utilizatorul poate selecta dată de sfarsit mai mică decât inceput.

Soluție:
- Validare date automatică
- Auto-corectare cu +1 lună

Branch: issue/2-datevalidation
```
- **Labels:** bug, resolved

#### Issue #8
- **Titlu:** 🐛 Problemă export Excel pe fișiere mari
- **Body:**
```
Export Excel se blocează pe >10.000 rânduri.

Trebuie:
- Optimizare memorie (streaming)
- Bară progres
- Posibilitate cancelez
```
- **Labels:** bug, performance

#### Issue #9
- **Titlu:** 🐛 ComboBox nu se actualizează după adăugare obiectiv
- **Body:**
```
Dacă adaug obiectiv din alta locație, ComboBox pe rapoarte nu se actualiza.

Soluție:
- Event notificări (INotifyCollectionChanged)
- Refresh observabil după insert
```
- **Labels:** bug, ui

---

### ✨ CARACTERISTICI (Features)

#### Issue #3
- **Titlu:** ✨ Raport 2: Costuri pe material (agregat)
- **Body:**
```
**STATUS: ✅ IMPLEMENTAT**

Creare raport nou care agregrează costuri pe material.

Includ:
- RaportMaterial model
- GetRaportMaterial() în DataService
- Grupare pe material cu suma costuri
- Ordonare descrescătoare după cost

Branch: feature/3-material-report
```
- **Labels:** feature, resolved

#### Issue #4
- **Titlu:** ✨ Adaugă grafice (Chart) pentru consum materiale
- **Body:**
```
Vizualizare cu grafice:
- Consum pe obiectiv (pie chart)
- Cost pe material (bar chart)
- Tendință consum (line chart)

Branch: feature/4-charts
```
- **Labels:** feature, enhancement

#### Issue #5
- **Titlu:** ✨ Conectare la bază de date SQL Server
- **Body:**
```
Înlocuiți mock data cu conexiune reală SQL Server.

Includ:
- Connection string din app.config
- CRUD operations
- Stored procedures pentru rapoarte
- Migration script pentru tabele
```
- **Labels:** feature, database

#### Issue #6
- **Titlu:** ✨ Export în format PDF
- **Body:**
```
Export rapoarte în PDF cu layout profesional.

Includ:
- iTextSharp sau SelectPdf
- Header cu logo
- Tabel cu date formatate
- Total calculat
- Footer cu dată/pagină
```
- **Labels:** feature, export

#### Issue #7
- **Titlu:** ✨ Importare date din Excel
- **Body:**
```
Citire date din fișier Excel.

Includ:
- Upload Excel cu validare
- Parsare rânduri
- Validare date (duplicate, format)
- Importare în bază de date
- Raport cu rezultate
```
- **Labels:** feature, import

---

### 📚 DOCUMENTAȚIE (Documentation)

#### Issue #10
- **Titlu:** 📚 Ghid utilizator complet (Wiki GitHub)
- **Body:**
```
Creare wiki complet cu:
- Instalare și setup
- Ghid utilizator (screenshots)
- Tutorial rapoarte
- FAQ
- Troubleshooting
```
- **Labels:** documentation, wiki

#### Issue #11
- **Titlu:** 📚 Screenshots și tutoriale în README
- **Body:**
```
Adaugă în README:
- Screenshots UI
- Demo GIF cu workflow
- Tabel cu rapoarte
- Links la wiki
```
- **Labels:** documentation, readme

#### Issue #12
- **Titlu:** 📚 Documentație API pentru servicii
- **Body:**
```
Generare XML docs și markdown pentru:
- DataService metode
- ExcelExportService
- ViewModels properties
- Models classes
```
- **Labels:** documentation, api

---

### 🔧 ÎMBUNĂTĂȚIRI (Enhancements)

#### Issue #13
- **Titlu:** 🔧 Interfață Dark Mode
- **Body:**
```
Adaugă tema întunecată cu toggle:
- Dark color palette
- Local storage pentru preferință
- Smooth transition
```
- **Labels:** enhancement, ui

#### Issue #14
- **Titlu:** 🔧 Localizare: Engleză + Română
- **Body:**
```
Suport pentru 2 limbi:
- Resource files (.resx)
- Language selector în UI
- Persistență alegere
```
- **Labels:** enhancement, localization

#### Issue #15
- **Titlu:** 🔧 Unit tests pentru DataService
- **Body:**
```
Coverage teste:
- GetRaportConsum filters
- GetRaportMaterial aggregation
- Date validation
```
- **Labels:** enhancement, testing

#### Issue #16
- **Titlu:** 🔧 Autentificare și roli utilizator
- **Body:**
```
Adaugă sistem de access control:
- Login/Register
- Roli: Admin, User, Viewer
- Permisiuni pe operații
```
- **Labels:** enhancement, security

#### Issue #17
- **Titlu:** 🔧 Criptare date sensibile
- **Body:**
```
Securitate date:
- Criptare connection string
- Criptare sensitive fields
- HTTPS enforced
```
- **Labels:** enhancement, security

---

## 📊 Rezumat

**Total: 17 Issues**
- ✅ 3 Rezolvate (Ready)
- ⏳ 6 Planificate (Next)
- 🔴 8 Backlog

## 🚀 Pasul Următor

După creare issues, fă push pe GitHub:
```bash
cd c:\Users\User\Desktop\practica\REDConstruct
git push -u origin main
```

Gata! 🎉
