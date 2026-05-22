# Jurnal Schimbări

Toate schimbările notabile la proiectul Calculator Materiale Termoizolație vor fi documentate în acest fișier.

## [1.0.0] - 2026-05-21

### Adăugat
- Configurare și structură inițială proiect
- Implementare arhitectură MVVM
- Modele entitate (Client, Obiectiv, Material, CalculConsum, Comanda, DetaliiComanda)
- Strat acces date Entity Framework Core
- Tipare Generic Repository și Unit of Work
- Clase ajutoare MVVM (RelayCommand, ViewModelBase)
- Clasă utilitară Calculator Materiale
- ViewModels inițiale (ClientViewModel, MaterialViewModel, ComandaViewModel)
- Configurare bază de date și date inițiale
- Fișiere configurare aplicație (appsettings.json)
- Documentație cuprinzătoare proiect
  - Document cerințe cu specificații funcționale/non-funcționale
  - Diagrama Relații Entități (Mermaid)
  - Diagrama Cazuri de Utilizare (PlantUML)
  - Documentație structură proiect

### În Curs
- Implementare UI WPF (Views)
- Implementări ViewModels complete
- ViewModels suplimentare (MainViewModel, ObiectivViewModel, CalculConsumViewModel)

### Planificat (Faza 2)
- [ ] Design UI XAML
- [ ] Fereastră gestionare clienți
- [ ] Fereastră gestionare proiecte
- [ ] Fereastră gestionare materiale
- [ ] Interfață creare calcule
- [ ] Gestionare și creare comenzi
- [ ] Funcționalitate export PDF
- [ ] Modul raportări

### Planificat (Faza 3)
- [ ] Autentificare utilizator
- [ ] Control acces bazat pe roluri
- [ ] Jurnalizare audit
- [ ] UI setări aplicație
- [ ] Validare date
- [ ] Gestionare erori și feedback utilizator

### Planificat (Faza 4)
- [ ] Căutare și filtrare avansate
- [ ] Import/export în masă
- [ ] Optimizare performanță
- [ ] Suport multi-limbă
- [ ] Aplicație companion pentru mobil

---

## Note

### Bază de Date
- Folosește LocalDB implicit în dezvoltare
- Configurată pentru SQL Server 2019+
- Suportă mai multe seturi rezultate active (MARS)

### Arhitectură
- Urmărește strict tiparul MVVM
- Folosește async/await peste tot
- Implementează injecție de dependență

### Standarde
- Caracteristici limbă C# 12
- Framework .NET 8
- Entity Framework Core 8
- Tipuri referință cu valoare nulă activate
