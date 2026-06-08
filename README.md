# Calculator Materiale Termoizolatie - RED Construct

Aplicatie desktop WPF pentru calculul materialelor de termoizolatie, gestiunea clientilor, obiectivelor, comenzilor, devizelor si rapoartelor.

Autor: Alexandru Plamadeala, UTM anul 4, PAPP-221  
Perioada practica: 21 aprilie - 13 iunie 2026  
Tehnologii: C# WPF .NET 8, Entity Framework Core, SQL Server LocalDB, QuestPDF, ClosedXML

## Functionalitati

- Calcul materiale termoizolatie pe baza suprafetei.
- CRUD clienti.
- CRUD obiective, cu validare client selectat si suprafata mai mare decat 0.
- Salvare calcule in tabela `CalculConsum`.
- Generare deviz cu manopera 35% si TVA 20%.
- Salvare atomica a devizului ca `Comanda` + `DetaliiComanda`.
- Export PDF cu QuestPDF.
- Rapoarte filtrate dupa data si material.
- Top 5 materiale dupa consum.
- Export Excel cu ClosedXML.
- Splash screen la pornire.

## Formule

- Polistiren = suprafata * 1.10
- Dibluri = Ceiling(suprafata * 6 * 1.10)
- Adeziv = Ceiling(suprafata / 6)
- Plasa = suprafata * 1.15
- Tencuiala = Ceiling(suprafata / 4)
- Amorsa = Ceiling(suprafata / 10)
- Manopera = Total materiale * 0.35
- TVA = (Total materiale + Manopera) * 0.20

## Configurare SQL Server

Aplicatia foloseste SQL Server LocalDB prin connection string:

```json
"RedConstructDB": "Server=(localdb)\\MSSQLLocalDB;Database=RedConstructDB;Trusted_Connection=True;TrustServerCertificate=True;"
```

Scriptul SQL se afla in:

```text
CalculatorMateriale/Database/RedConstructDB_Script.sql
```

La pornire, EF Core creeaza schema prin `EnsureCreated()`. Pentru rulare de productie se recomanda migrarea la EF Core Migrations.

## Rulare

1. Instaleaza .NET 8 SDK.
2. Instaleaza SQL Server LocalDB sau SQL Server Express.
3. Deschide proiectul `CalculatorMateriale/CalculatorMateriale.csproj`.
4. Ruleaza:

```powershell
dotnet restore
dotnet build
dotnet run --project .\CalculatorMateriale\CalculatorMateriale.csproj
```

## Observatii verificare

Pe masina curenta, comenzile `dotnet` si `git` nu sunt disponibile in PATH, deci build-ul si istoricul GitHub nu pot fi validate local din terminal. Codul a fost verificat static si actualizat pentru cerintele aplicatiei.
