# 📋 Ghid Issues & Branches - RED Construct

## 🎯 Starea Curentă a Issues

### 🐛 ERORI (În Lucru)
| Issue | Branch | Stare | Descriere |
|-------|--------|-------|----------|
| #1 | `issue/1-datagrid-refresh` | ✅ Rezolvat | Optimizare reîmprospătare DataGrid |
| #2 | `issue/2-datevalidation` | ✅ Rezolvat | Validare dată adăugată |
| #8 | `issue/8-excel-export` | 📝 În curs | Probleme export Excel |

### ✨ CARACTERISTICI (În Lucru)
| Feature | Branch | Stare | Descriere |
|---------|--------|-------|----------|
| #3 | `feature/3-material-report` | ✅ Implementat | Raport agregate pe material |
| #4 | `feature/4-charts` | 📝 Planificat | Vizualizare grafice |
| #5 | `feature/5-database` | 📝 Planificat | Conexiune SQL Server |

### 📚 DOCUMENTAȚIE
| Doc | Branch | Stare | Descriere |
|-----|--------|-------|----------|
| #10 | `docs/10-user-guide` | 📝 Planificat | Ghid utilizator |
| #11 | `docs/11-screenshots` | 📝 Planificat | Screenshots în README |

---

## 🔧 Cum Să Lucrezi cu Issues

### 1. **Creează Branch pentru Issue de pe Main**
```bash
git checkout main
git pull
git checkout -b issue/10-descriere
```

### 2. **Fă Schimbări și Commit cu Referință la Issue**
```bash
git add .
git commit -m "Rezolvă #10: Descriere scurtă a rezolvării"
```

### 3. **Trimite Branch pe GitHub**
```bash
git push -u origin issue/10-descriere
```

### 4. **Creează Pull Request (pe GitHub)**
1. Mergi la repository-ul GitHub
2. Click pe tab-ul "Pull Requests"
3. Click pe "New Pull Request"
4. Selectează base: `main`, compare: `issue/10-descriere`
5. Adaugă descriere și referință: "Closes #10"

### 5. **Îmbină După Aprobare**
```bash
git checkout main
git pull
git merge issue/10-descriere
git push origin main
```

---

## 📊 Comenzi Git pentru Issues

### Listează Toate Branch-urile
```bash
git branch -a
```

### Listează Commit-uri după Issue
```bash
git log --all --grep="#1"  # Arată toate referințele #1
```

### Vezi Graficul Istoric Commit-uri
```bash
git log --all --graph --oneline --decorate
```

### Șterge Branch Îmbinat
```bash
git branch -d issue/1-datagrid-refresh
git push origin --delete issue/1-datagrid-refresh
```

---

## 📝 Format Mesaj Commit

```
Tip: #NumarIssue - Descriere Scurtă

[Opțional] Explicație detaliată

Rezolvă #NumarIssue
```

### Exemple:
```
Rezolvă #1: Îmbunătățire reîmprospătare DataGrid cu update UI forțat
Caracteristică #3: Adaugă raport consum material
Docs #10: Actualizare ghid utilizator cu screenshots
Refactoring: Simplificare metode DataService
```

---

## 🔄 Convenție Nume Branch

| Tip | Format | Exemplu |
|-----|--------|----------|
| Corectare Eroare | `issue/NUMAR-descriere` | `issue/1-reimprospatare-datagrid` |
| Caracteristică | `feature/NUMAR-descriere` | `feature/3-raport-material` |
| Documentație | `docs/NUMAR-descriere` | `docs/10-ghid-utilizator` |
| Refactoring | `refactor/descriere` | `refactor/simplificare-datagrid` |

---

## ✅ Rezumat Workflow

1. **Creează branch** de pe `main` cu numărul issue-ului
2. **Fă schimbări** și commit cu `Rezolvă #N` sau `Caracteristică #N`
3. **Trimite branch** pe GitHub
4. **Creează Pull Request** cu descriere
5. **Revizuiește & Testează** schimbările
6. **Îmbină** pe main când e aprobat
7. **Șterge** branch-ul feature

---

## 🚀 Exemple Start Rapid

### Corectează o Eroare
```bash
git checkout main
git checkout -b issue/5-descriere-eroare
# Fă schimbări
git add .
git commit -m "Rezolvă #5: Descriere rezolvării erorii"
git push -u origin issue/5-descriere-eroare
```

### Adaugă o Caracteristică
```bash
git checkout main
git checkout -b feature/6-caracteristica-noua
# Adaugă fișiere/cod noi
git add .
git commit -m "Caracteristică #6: Descriere caracteristică nouă"
git push -u origin feature/6-caracteristica-noua
```

### Vezi Istoricul Issue
```bash
git log --grep="#5"  # Arată toți commit-ii referind issue #5
```

---

## 📌 Stare Curentă

```
main (5 commits)
├── issue/1-datagrid-refresh (Rezolvă #1) ✅
├── issue/2-datevalidation (Rezolvă #2) ✅
├── feature/3-material-report (Caracteristică #3) ✅
├── feature/4-charts (Caracteristică #4) ⏳
└── docs/6-user-guide (Docs #6) ⏳
```

Gata să începi lucrul pe issues? Alege o issue din listă și urmează workflow-ul! 🎉
