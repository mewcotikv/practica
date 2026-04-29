# RED Construct - Sistem Rapoarte WPF

Proiect C# WPF pentru managementul rapoartelor de consum materiale în construcții.

## Caracteristici

### Raport 1: Consum Materiale pe Obiectiv
- **DatePicker**: Filtrare pe perioadă (Data Început - Data Sfârșit)
- **ComboBox**: Selectare obiectiv (Toate sau selectare specifică)
- **DataGrid**: Afișare date cu coloane:
  - Obiectiv
  - Material
  - Cantitate
  - Cost
  - Data Calcul
- **Export Excel**: Buton pentru exportul raportului în format Excel (ClosedXML)

## Structură Proiect

```
REDConstruct/
├── Views/
│   └── RapoarteView.xaml          # UI pentru Raport 1
├── ViewModels/
│   └── RapoarteViewModel.cs       # ViewModel cu logica raportului
├── Models/
│   ├── Obiectiv.cs                # Model pentru obiectiv
│   ├── ConsuMaterial.cs           # Model pentru consum material
│   └── RaportConsum.cs            # Model pentru raport
├── Services/
│   ├── DataService.cs             # Serviciu pentru date (mock)
│   └── ExcelExportService.cs      # Serviciu pentru export Excel
├── App.xaml                        # Application root
├── MainWindow.xaml                 # Main window
└── REDConstruct.csproj            # Project file
```

## Dependențe

- .NET 6.0 (Windows Desktop)
- ClosedXML 0.99.0 (pentru export Excel)

## Cum se utilizează

1. **Selectare dată**: Folosiți DatePickerele pentru a alege perioada dorit
2. **Selectare obiectiv**: Alegeți "Toate" sau un obiectiv specific din ComboBox
3. **Filtrare**: Apăsați butonul "Filtrează" pentru a actualiza datele
4. **Export**: Apăsați "📊 Export Excel" pentru a descărca raportul în format Excel

## Date de test

Proiectul conține date mock pentru 3 obiective:
- Obiectiv A
- Obiectiv B
- Obiectiv C

Cu consumuri diferite de materiale (Ciment, Nisip, Oțel, Pietre).

## Compilare și Rulare

```bash
dotnet restore
dotnet build
dotnet run
```

## Extensii viitoare

- Conexiune la bază de date reală
- Rapoarte suplimentare
- Importare date din surse externe
- Grafice și analize
