# 📋 Git Issues - RED Construct

## Cum să creezi issues în terminal

### 1. Vizualizează o issue locală
```bash
git log --oneline --all --decorate --grep="#1"
```

### 2. Creează branch pentru issue
```bash
git checkout -b issue/1-descriere
```

### 3. Commit cu referință la issue
```bash
git commit -m "Rezolvă #1: descriere schimbare"
```

### 4. Îmbină issue (merge)
```bash
git checkout main
git merge issue/1-descriere
```

## Issues planiuite pentru RED Construct

### 🐛 ERORI (Urgent)
- [x] #1 - DataGrid nu reîmprospătează datele - **REZOLVAT**
- [x] #2 - DatePicker nu validează Data Sfarsit < Data Inceput - **REZOLVAT**
- [ ] #8 - Problema cu export Excel pe fișiere mari
- [ ] #9 - ComboBox nu se actualizează după adăugare obiectiv nou

### ✨ CARACTERISTICI (Prioritate înaltă)
- [x] #3 - Raport 2: Costuri pe material - **IMPLEMENTAT**
- [ ] #4 - Adaugă grafice (Chart) pentru consum materiale
- [ ] #5 - Conectare la bază de date SQL Server
- [ ] #6 - Export în format PDF
- [ ] #7 - Importare date din Excel

### 📚 DOCUMENTAȚIE
- [ ] #10 - Ghid utilizator complet (Wiki GitHub)
- [ ] #11 - Screenshots și tutoriale în README
- [ ] #12 - Documentație API pentru servicii

### 🔨 ÎMBUNĂTĂȚIRI
- [ ] #13 - Interfață Dark Mode
- [ ] #14 - Localizare: Engleză + Română
- [ ] #15 - Teste unitare pentru DataService
- [ ] #16 - Autentificare și roli utilizator
- [ ] #17 - Criptare date sensibile
