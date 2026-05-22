# DOCUMENT CERINȚE
## Calculator Materiale Termoizolație - RED Construct

**Nume Proiect:** Calculator Materiale Termoizolație  
**Companie:** RED Construct  
**Tip Aplicație:** Aplicație Desktop WPF (.NET 8)  
**Arhitectură:** Tipar MVVM  
**Bază de Date:** SQL Server (sau compatibilă cu Entity Framework Core)  
**Versiune Document:** 1.0  
**Dată:** 21 mai 2026

---

## 1. PREZENTARE

Aplicația Calculator Materiale Termoizolație este o soluție desktop proiectată pentru a eficientiza procesul de calcul al necesarului de materiale de termoizolație pentru proiecte de construcție. Permite RED Construct să gestioneze clienți, proiecte construcție (obiective), materiale, calculate consum și genereze comenzi cu prețuri detaliate.

---

## 2. CERINȚE FUNCȚIONALE

### 2.1 Gestionare Clienți (Gestiunea Clientilor)

**FR-1.1: Adaugă Client**
- Sistemul trebuie să permită utilizatorilor să adauge clienți noi cu următoarele informații:
  - Nume Companie (Nume)
  - CUI (Cod de Înregistrare Fiscală)
  - Adresă (Adresa)
  - Telefon (Telefon)
  - Email
  - Localitate (Localitate)
  - Cod Poștal (CodPostal)
- Sistemul trebuie să valideze câmpurile obligatorii
- Sistemul trebuie să prevină intrări CUI duplicate

**FR-1.2: Vizualizează Clienți**
- Sistemul trebuie să afișeze toți clienții înregistrați într-o listă paginată
- Sistemul trebuie să afișeze informații cheie client

**FR-1.3: Editează Client**
- Sistemul trebuie să permită modificarea informațiilor client
- Sistemul trebuie să actualizeze datele client în baza de date

**FR-1.4: Șterge Client**
- Sistemul trebuie să permită ștergerea clienților cu confirmare
- Sistemul trebuie să prevină ștergerea dacă client are proiecte active

**FR-1.5: Caută Clienți**
- Sistemul trebuie să suporte căutare după:
  - Nume companie
  - CUI
  - Localitate
  - Email

---

### 2.2 Gestionare Proiecte (Gestiunea Obiectivelor)

**FR-2.1: Adaugă Proiect**
- Sistemul trebuie să permită utilizatorilor să adauge proiecte construcție noi cu:
  - Nume Proiect (Denumire)
  - Client Asociat
  - Suprafață în m² (SuprafataM2)
  - Descriere (Descriere)
  - Adresă
  - Localitate
  - Status (Activ, Închis, Suspendat)
- Sistemul trebuie să valideze suprafața ca zecimal pozitiv

**FR-2.2: Vizualizează Proiecte**
- Sistemul trebuie să afișeze toate proiectele cu filtre:
  - După Client
  - După Status
  - După Interval Dată

**FR-2.3: Editează Proiect**
- Sistemul trebuie să permită modificarea detaliilor proiect
- Sistemul trebuie să actualizeze data finalizării dacă este cazul

**FR-2.4: Șterge Proiect**
- Sistemul trebuie să permită ștergere cu confirmare
- Sistemul trebuie să prevină ștergere dacă proiect are calcule active

**FR-2.5: Vizualizează Detalii Proiect**
- Sistemul trebuie să afișeze:
  - Toate calculele asociate
  - Consum total per material
  - Comenzi asociate

---

### 2.3 Gestionare Materiale (Gestiunea Materialelor)

**FR-3.1: Adaugă Material**
- Sistemul trebuie să permită utilizatorilor să adauge materiale termoizolație cu:
  - Nume (Denumire)
  - Tip (Tip): Polistiren, Adeziv, Dibleuri, Plasă, Tencuiala, Amorsa
  - Descriere
  - Preț Unitar (Pret)
  - Unitate (buc, kg, l, m², etc.)
  - Densitate (DensitateKgM3)
  - Conductivitate Termică (ConductivitateTermica) - W/(m·K)
  - Stoc Disponibil (StocDisponibil)

**FR-3.2: Vizualizează Materiale**
- Sistemul trebuie să afișeze materiale sortate după:
  - Tip
  - Nume
  - Disponibilitate
- Sistemul trebuie să afișeze proprietăți termice

**FR-3.3: Editează Material**
- Sistemul trebuie să permită modificarea proprietăților și prețurilor material

**FR-3.4: Șterge Material**
- Sistemul trebuie să permită ștergere cu confirmare
- Sistemul trebuie să prevină ștergere dacă material este în uz

**FR-3.5: Populare Materiale Implicite**
- Sistemul trebuie să pre-încarce materiale termoizolație comune:
  - Polistiren Expandat (XPS) 100mm, 80mm
  - Adeziv Polistiren 25kg
  - Dibleuri plastic pentru polistiren
  - Plasă fibră sticlă
  - Tencuiala minerală 25kg
  - Apretură aderență 10l

---

### 2.4 Calcul Consum (Calcul Consum)

**FR-4.1: Creează Calcul**
- Sistemul trebuie să permită utilizatorilor să:
  - Selecteze proiect (Obiectiv)
  - Selecteze material
  - Specifice grosime (Grosime) în mm (pentru polistiren)
  - Introduc consum per m² (ConsumPeM2)
  - Opțional: specifice eficiență aplicare/randament (Randament)

**FR-4.2: Calcul Automat Consum**
- Sistemul trebuie să calculeze automat:
  - Consum Total = Suprafață × Consum per m²
  - Preț Total = Consum Total × Preț Unitar
- Sistemul trebuie să țină cont de eficiență/randament dacă specificat

**FR-4.3: Rate Consum Predefinite**
- Sistemul trebuie să ofere rate consum implicite pentru materiale standard:
  - Polistiren 100mm: X kg/m²
  - Adeziv: 5 kg/m²
  - Dibleuri: 4 per m²
  - Plasă: 1.1 m² (cu suprapunere)
  - Tencuiala: Y kg/m²
  - Apretură: Z l/m²

**FR-4.4: Stochează Calcule**
- Sistemul trebuie să salveze calcule cu:
  - Data calcul
  - Toți parametrii de intrare
  - Rezultate calculate
  - Note/Observații

**FR-4.5: Vizualizează Detalii Calcul**
- Sistemul trebuie să afișeze:
  - Rezumat consum per material
  - Total material necesar
  - Estimare cost total
  - Posibilitate export PDF

**FR-4.6: Export Calcul**
- Sistemul trebuie să genereze raport PDF cu:
  - Detalii client
  - Detalii proiect
  - Tabel consum material
  - Costuri totale
  - Timestamp

---

### 2.5 Gestionare Comenzi (Gestiunea Comenzilor)

**FR-5.1: Creează Comandă**
- Sistemul trebuie să permită utilizatorilor să:
  - Creeze comandă nouă (Comanda)
  - Asocieze cu client și proiect opțional
  - Seteze dată comandă (auto-populată cu data curentă)
  - Seteze dată livrare
  - Inițializeze cu status "Nouă" (Noua)

**FR-5.2: Adaugă Articole Comandă**
- Sistemul trebuie să permită adăugare materiale la comandă (DetaliiComanda):
  - Selecteze material
  - Specifice cantitate
  - Auto-populare preț unitar
  - Calcul total linie
  - Opțional: aplicare reducere nivel linie

**FR-5.3: Adăugare Bulk din Calcul**
- Sistemul trebuie să permită creare rapidă comandă prin:
  - Selectare calcul anterior
  - Import materiale și cantități din calcul

**FR-5.4: Aplicare Modificatoare Preț**
- Sistemul trebuie să suporte:
  - Reducere comandă globală (procent sau fix)
  - Calcul TVA (implicit 19%)
  - Calcul automat total

**FR-5.5: Gestionare Status Comandă**
- Sistemul trebuie să suporte tranzițiile status:
  - Nouă → Confirmată → În Pregătire → Livrată
  - Nouă → Anulată (oricând)

**FR-5.6: Vizualizează Detalii Comandă**
- Sistemul trebuie să afișeze:
  - Informații client
  - Articole comandă cu prețuri
  - Rezumat reducere
  - Detalii TVA
  - Valoare totală

**FR-5.7: Tipărire Comandă**
- Sistemul trebuie să genereze factură tipăribil cu:
  - Număr și dată comandă
  - Detalii client
  - Proiect (dacă asociat)
  - Materiale itemizate
  - Detalii prețuri
  - Dată livrare

**FR-5.8: Export Comandă**
- Sistemul trebuie să genereze PDF cu formatare profesională:
  - Branding RED Construct
  - Toți detalii comandă
  - Câmp semnătură client
  - Termeni plată (dacă configurați)

---

### 2.6 Raportări (Rapoarte)

**FR-6.1: Raport Comenzi pe Client**
- Sistemul trebuie să genereze raport arătând:
  - Lista clienți cu valori comenzi totale
  - Număr comenzi per client
  - Filtru interval dată
  - Venit total per client

**FR-6.2: Raport Consum Material**
- Sistemul trebuie să genereze raport arătând:
  - Utilizare material pe toate proiectele
  - Cantități totale necesare
  - Costuri totale per material
  - Comparație cu stoc disponibil

**FR-6.3: Raport Venituri**
- Sistemul trebuie să genereze raport arătând:
  - Venit total după interval dată
  - Venit per client
  - Venit per proiect
  - Comenzi nelivirate (nu încă livrate)

---

### 2.7 Setări Aplicație (Parametri Aplicatie)

**FR-7.1: Configurare TVA**
- Sistemul trebuie să permită configurare procent TVA (implicit 19%)

**FR-7.2: Rate Consum Implicite**
- Sistemul trebuie să permită personalizare rate consum implicite pentru fiecare tip material

**FR-7.3: Gestionare Utilizatori**
- Sistemul trebuie să suporte conturi utilizator cu roluri (Admin, Operator, Vizualizator)
- Sistemul trebuie să urmărească acțiuni utilizator pentru scopuri audit

---

## 3. CERINȚE NON-FUNCȚIONALE

### 3.1 Performanță

**NFR-1.1:** Sistemul trebuie să încarce lista clienți (100+ înregistrări) în < 1 secundă  
**NFR-1.2:** Sistemul trebuie să calculeze consum pentru orice material în < 500ms  
**NFR-1.3:** Sistemul trebuie să genereze rapoarte PDF în < 3 secunde  
**NFR-1.4:** Sistemul trebuie să gestioneze operații bază date concurente fără corupere date

### 3.2 Ușurință Utilizare

**NFR-2.1:** Aplicația trebuie să aibă UI WPF intuitiv cu principii Material Design  
**NFR-2.2:** Toate câmpurile numerice trebuie să suporte atât punct cât și virgulă ca separator zecimal  
**NFR-2.3:** Sistemul trebuie să ofere feedback calcul real-time (fără reîmprospătare manuală)  
**NFR-2.4:** Sistemul trebuie să implementeze comenzi rapidă navigare tastatură  
**NFR-2.5:** Sistemul trebuie să ofere mesaje erori utile în limba română

### 3.3 Fiabilitate

**NFR-3.1:** Sistemul trebuie să mențină integritate date cu tranzacții conformi ACID  
**NFR-3.2:** Sistemul trebuie să valideze toată intrarea utilizator înainte operații bază date  
**NFR-3.3:** Sistemul trebuie să ofere funcționalitate backup automat  
**NFR-3.4:** Sistemul trebuie să gestioneze eșecuri conexiune bază date abil cu logică retry

### 3.4 Securitate

**NFR-4.1:** Sistemul trebuie să folosească Autentificare Windows pentru autentificare utilizator  
**NFR-4.2:** Sistemul trebuie să cripteze date sensibile (contacte client, date financiare)  
**NFR-4.3:** Sistemul trebuie să aplice control acces bazat pe roluri  
**NFR-4.4:** Sistemul trebuie să mențină jurnale audit pentru toate modificările

### 3.5 Întreținere

**NFR-5.1:** Codul trebuie să urmeze strict tipar MVVM  
**NFR-5.2:** Codul trebuie să includ documentație XML cuprinzătoare  
**NFR-5.3:** Codul trebuie să urmeze convenții denumire C# (PascalCase pentru clase, camelCase pentru câmpuri)  
**NFR-5.4:** Structura proiect trebuie să separe clar responsabilități (Models, Views, ViewModels, Data, Helpers)

### 3.6 Compatibilitate

**NFR-6.1:** Aplicația trebuie să ruleze pe Windows 10 și Windows 11  
**NFR-6.2:** Aplicația trebuie să suporte runtime .NET 8  
**NFR-6.3:** Baza de date trebuie să fie compatibilă cu SQL Server 2019+ sau LocalDB

### 3.7 Localizare

**NFR-7.1:** UI trebuie să suporte limba română (ro-RO)  
**NFR-7.2:** Formatare numerică trebuie să urmeze convenții române  
**NFR-7.3:** Formatare dată trebuie să urmeze convenții române (dd/MM/yyyy)

---

## 4. ENTITĂȚI BAZĂ DE DATE

### 4.1 Client (CLIENTI)
- **IdClient** (PK): Integer
- **Nume**: String, Obligatoriu, Max 100
- **CUI**: String, Obligatoriu, Max 50, Unic
- **Adresa**: String, Max 100
- **Telefon**: String, Max 20
- **Email**: String, Max 100
- **Localitate**: String, Max 50
- **CodPostal**: String, Max 10
- **DataInregistrare**: DateTime

### 4.2 Proiect (OBIECTIV)
- **IdObiectiv** (PK): Integer
- **Denumire**: String, Obligatoriu, Max 150
- **IdClient** (FK): Integer
- **SuprafataM2**: Decimal, Obligatoriu, > 0
- **Descriere**: String, Max 200
- **Adresa**: String, Max 100
- **Localitate**: String, Max 50
- **DataCrearii**: DateTime
- **DataFinalizarii**: DateTime, Nullable
- **Status**: String (Activ, Închis, Suspendat)

### 4.3 Material (MATERIALE)
- **IdMaterial** (PK): Integer
- **Denumire**: String, Obligatoriu, Max 100
- **Tip**: String, Obligatoriu (Polistiren, Adeziv, Dibleuri, Plasă, Tencuiala, Amorsa)
- **Descriere**: String, Max 200
- **Pret**: Decimal, Obligatoriu
- **Unitate**: String, Obligatoriu (buc, kg, l, m², etc.)
- **DensitateKgM3**: Decimal, Obligatoriu
- **ConductivitateTermica**: Decimal, Obligatoriu
- **StocDisponibil**: Integer
- **DataAdaugarii**: DateTime
- **Activ**: Boolean

### 4.4 Calcul Consum (CALCUL_CONSUM)
- **IdCalculConsum** (PK): Integer
- **IdObiectiv** (FK): Integer
- **IdMaterial** (FK): Integer
- **ConsumPeM2**: Decimal, Obligatoriu
- **ConsumTotal**: Decimal, Obligatoriu
- **PretUnitar**: Decimal, Obligatoriu
- **PretTotal**: Decimal
- **Grosime**: Integer, Nullable (mm)
- **Randament**: Decimal, Nullable (%)
- **DataCalcul**: DateTime
- **Observatii**: String, Max 200

### 4.5 Comandă (COMENZI)
- **IdComanda** (PK): Integer
- **IdClient** (FK): Integer
- **IdObiectiv** (FK): Integer, Nullable
- **DataComanda**: DateTime
- **DataLivrare**: DateTime, Nullable
- **Status**: String (Nouă, Confirmată, În Pregătire, Livrată, Anulată)
- **ValoareTotala**: Decimal
- **TVA**: Decimal, Nullable
- **Reducere**: Decimal, Nullable
- **Observatii**: String, Max 300

### 4.6 Detalii Comandă (DETALII_COMANDA)
- **IdDetaliiComanda** (PK): Integer
- **IdComanda** (FK): Integer
- **IdMaterial** (FK): Integer
- **Cantitate**: Decimal, Obligatoriu
- **PretUnitar**: Decimal, Obligatoriu
- **PretTotal**: Decimal
- **ProcentReducere**: Decimal, Nullable

---

## 5. RELAȚII

- **Client → Obiective**: Unu-la-Mulți (1 client are multe proiecte)
- **Client → Comenzi**: Unu-la-Mulți (1 client plasează multe comenzi)
- **Obiectiv → CalculConsum**: Unu-la-Mulți (1 proiect are multe calcule)
- **Obiectiv → Comenzi**: Unu-la-Mulți (1 proiect are multe comenzi)
- **Material → CalculConsum**: Unu-la-Mulți (1 material în multe calcule)
- **Material → DetaliiComanda**: Unu-la-Mulți (1 material în multe detalii comandă)
- **Comanda → DetaliiComanda**: Unu-la-Mulți (1 comandă are multe articole linie)

---
