# Teste Calculator Materiale - RED Construct

| ID | Scenariu | Date intrare | Rezultat asteptat | Status |
|---|---|---|---|---|
| TC01 | Suprafata invalida 0 | Suprafata = 0 | Mesaj eroare, calculul nu se executa | Implementat |
| TC02 | Suprafata negativa | Suprafata = -5 | Mesaj eroare, calculul nu se executa | Implementat |
| TC03 | Polistiren | Suprafata = 100 | Consum = 110 mp | Implementat |
| TC04 | Dibluri | Suprafata = 100 | Consum = Ceiling(100 * 6 * 1.10) = 660 buc | Implementat |
| TC05 | Adeziv | Suprafata = 100 | Consum = Ceiling(100 / 6) = 17 saci/kg dupa unitate | Implementat |
| TC06 | Plasa | Suprafata = 100 | Consum = 115 mp | Implementat |
| TC07 | Tencuiala | Suprafata = 100 | Consum = Ceiling(100 / 4) = 25 saci | Implementat |
| TC08 | Amorsa | Suprafata = 100 | Consum = Ceiling(100 / 10) = 10 l | Implementat |
| TC09 | Pret total | Consum = 110, pret = 150 | Total = 16500 MDL | Implementat |
| TC10 | TVA calculator | Total = 16500 | TVA inclus = 19800 MDL | Implementat |
| TC11 | Salvare calcul | Exista rezultat in grid | Randuri salvate in `CalculConsum` | Implementat |
| TC12 | Clienti - adaugare | Click Adauga | Client nou apare in DataGrid | Implementat |
| TC13 | Clienti - cautare | Text nume/CUI | Lista se filtreaza | Implementat |
| TC14 | Clienti - stergere | Selectie + Sterge | Clientul selectat este sters dupa confirmare | Implementat |
| TC15 | Obiective - fara client | Client neselectat | Mesaj validare | Implementat |
| TC16 | Obiective - suprafata 0 | Suprafata = 0 | Mesaj validare | Implementat |
| TC17 | Obiective - adaugare | Client + suprafata valida | Obiectivul apare in DataGrid | Implementat |
| TC18 | Deviz - totaluri | Materiale = 1000 | Manopera = 350, TVA = 270, Total = 1620 | Implementat |
| TC19 | Deviz - tranzactie | Export PDF | Comanda si detaliile se salveaza atomic | Implementat |
| TC20 | Export PDF | Deviz cu materiale | PDF generat pe Desktop | Implementat |
| TC21 | Comenzi - status | Click Status | Noua -> Confirmata -> Finalizata -> Noua | Implementat |
| TC22 | Rapoarte - data | Interval selectat | Sunt afisate doar calculele din interval | Implementat |
| TC23 | Rapoarte - top 5 | Calcule existente | Lista top 5 materiale se actualizeaza | Implementat |
| TC24 | Export Excel | Rapoarte incarcate | Fisier `.xlsx` generat pe Desktop | Implementat |
| TC25 | Navigare | Meniu principal | Paginile se incarca in Frame | Implementat |
| TC26 | Build | `dotnet build` | Nu poate fi verificat local fara .NET SDK in PATH | Neverificat local |
