# 🚀 RAPID: Cum Să Faci Push + Issues pe GitHub în 5 Minute

## Pasul 1: Push Proiectul pe GitHub

### Metoda A: Prin VS Code (CEL MAI UȘOR)
1. Deschide **VS Code**
2. File → Open Folder → `c:\Users\User\Desktop\practica\REDConstruct`
3. Apesi **Ctrl+Shift+G** (Source Control)
4. Click pe butonul **"Publish to GitHub"** (dacă nu e, click "Initialize Repository")
5. Alegi "Publish to GitHub public repository"
6. Gata! 🎉

### Metoda B: Terminal cu ghcli (Dacă ai timp)
```bash
# Restart terminal și:
gh auth login
# Alegi: GitHub.com → HTTPS → Y
# Copiază device code și autentifică pe web

# Apoi:
cd c:\Users\User\Desktop\practica\REDConstruct
git push -u origin main
```

---

## Pasul 2: Creare Issues pe GitHub Web

După push, mergi pe: **https://github.com/mewcotikv/practica/issues**

### Click "New Issue" și crează acestea (copie-paste):

---

### ERORI (Bugs) - 4 Issues

#### 🐛 ISSUE 1
```
Title: 🐛 #1 - DataGrid nu se reîmprospătează după export
Labels: bug, resolved

Body:
**STATUS: ✅ REZOLVAT**

Problemă: DataGrid nu se reîmprospătează după export Excel.

✅ Soluție:
- Added UI refresh forțat
- Error handling în RapoarteViewModel  
- Validare date

📂 Branch: issue/1-datagrid-refresh
```

#### 🐛 ISSUE 2
```
Title: 🐛 #2 - DatePicker nu validează Data Sfarsit < Data Inceput
Labels: bug, resolved

Body:
**STATUS: ✅ REZOLVAT**

Problemă: User poate selecta dată sfarsit < inceput.

✅ Soluție:
- Validare date automatică
- Auto-corectare +1 lună

📂 Branch: issue/2-datevalidation
```

#### 🐛 ISSUE 8
```
Title: 🐛 #8 - Export Excel fișiere mari (>10k rânduri)
Labels: bug, performance

Body:
Export se blocează pe fișiere mari.

❓ Trebuie:
- Streaming memory optimization
- Progress bar
- Posibilitate cancelez
```

#### 🐛 ISSUE 9
```
Title: 🐛 #9 - ComboBox obiective nu se actualizează
Labels: bug, ui

Body:
Dacă adaug obiectiv din alta locație, ComboBox pe rapoarte nu se refresh.

❓ Trebuie:
- INotifyCollectionChanged events
- Refresh observabil după insert
```

---

### CARACTERISTICI (Features) - 5 Issues

#### ✨ ISSUE 3
```
Title: ✨ #3 - Raport 2: Costuri pe material (agregat)
Labels: feature, resolved

Body:
**STATUS: ✅ IMPLEMENTAT**

Raport care agregrează costuri pe material.

✅ Implementat:
- RaportMaterial model
- GetRaportMaterial() în DataService
- GroupBy + Sum costuri
- Ordonare descrescător

📂 Branch: feature/3-material-report
```

#### ✨ ISSUE 4
```
Title: ✨ #4 - Adaugă grafice (Charts) pentru consum
Labels: feature, enhancement

Body:
Vizualizări cu grafice:
- Pie chart: consum pe obiectiv
- Bar chart: cost pe material
- Line chart: tendință consum

📂 Branch: feature/4-charts
```

#### ✨ ISSUE 5
```
Title: ✨ #5 - Conectare la SQL Server
Labels: feature, database

Body:
Înlocuire mock data cu SQL real.

❓ Includ:
- Connection string management
- CRUD operations
- Stored procedures
- Migration scripts
```

#### ✨ ISSUE 6
```
Title: ✨ #6 - Export în format PDF
Labels: feature, export

Body:
Export rapoarte PDF cu layout profesional.

❓ Includ:
- Header cu logo
- Tabel formatat
- Total calculat
- Footer dată/pagină
```

#### ✨ ISSUE 7
```
Title: ✨ #7 - Importare date din Excel
Labels: feature, import

Body:
Upload și procesare Excel.

❓ Includ:
- Upload validation
- Parsare rânduri
- Duplicate check
- Importare DB
- Raport rezultate
```

---

### DOCUMENTAȚIE (Docs) - 3 Issues

#### 📚 ISSUE 10
```
Title: 📚 #10 - Ghid utilizator complet (Wiki)
Labels: documentation, wiki

Body:
Wiki complet cu:
- Instalare și setup
- Ghid utilizator cu screenshots
- Tutorial rapoarte
- FAQ și Troubleshooting
```

#### 📚 ISSUE 11
```
Title: 📚 #11 - Screenshots și tutoriale în README
Labels: documentation, readme

Body:
Update README cu:
- UI Screenshots
- Demo GIF workflow
- Feature table
- Links la wiki
```

#### 📚 ISSUE 12
```
Title: 📚 #12 - API Documentation
Labels: documentation, api

Body:
XML docs + Markdown pentru:
- DataService methods
- ExcelExportService
- ViewModels properties
- Models classes
```

---

### ÎMBUNĂTĂȚIRI (Enhancements) - 5 Issues

#### 🔧 ISSUE 13
```
Title: 🔧 #13 - Dark Mode Interface
Labels: enhancement, ui

Body:
Interfață Dark Mode cu toggle.

❓ Includ:
- Dark color palette
- Settings persistence
- Smooth transition
```

#### 🔧 ISSUE 14
```
Title: 🔧 #14 - Localizare: Engleză + Română
Labels: enhancement, localization

Body:
Suport 2 limbi.

❓ Includ:
- Resource files (.resx)
- Language selector
- Settings save
```

#### 🔧 ISSUE 15
```
Title: 🔧 #15 - Unit Tests DataService
Labels: enhancement, testing

Body:
Test coverage pentru logică.

❓ Teste:
- GetRaportConsum filters
- GetRaportMaterial aggregation
- Date validation
```

#### 🔧 ISSUE 16
```
Title: 🔧 #16 - Autentificare și Roli
Labels: enhancement, security

Body:
Access control sistem.

❓ Includ:
- Login/Register
- Roli: Admin, User, Viewer
- Permission-based operations
```

#### 🔧 ISSUE 17
```
Title: 🔧 #17 - Criptare Date Sensibile
Labels: enhancement, security

Body:
Securitate date.

❓ Includ:
- Criptare connection string
- Criptare sensitive fields
- HTTPS enforcement
```

---

## ✅ REZUMAT

```
17 ISSUES TOTAL:
✅ 3 Rezolvate (Ready)
⏳ 6 Planificate (Next Priority)
🔴 8 Backlog (Future)
```

## 🎯 FINAL STEPS

1. **Push pe GitHub**: Folosește VS Code (Publish to GitHub)
2. **Crează Issues**: Copie-paste títluri + body din mai sus
3. **Labels**: Adaug labels sugerate
4. **Done!** 🎉

---

## 📌 Pro Tip

Pentru mai rapid, poti folosi GitHub CLI după ce se instalează în noul terminal:

```bash
gh issue create --title "Titlul" --body "Descrierea" --label "label1,label2"
```

**Gata! Succes!** 💪
