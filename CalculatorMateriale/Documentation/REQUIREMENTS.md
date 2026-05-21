# REQUIREMENTS DOCUMENT
## Calculator Materiale Termoizolatie - RED Construct

**Project Name:** Calculator Materiale Termoizolatie  
**Company:** RED Construct  
**Application Type:** WPF Desktop Application (.NET 8)  
**Architecture:** MVVM Pattern  
**Database:** SQL Server (or Entity Framework Core compatible)  
**Document Version:** 1.0  
**Date:** May 21, 2026

---

## 1. OVERVIEW

The Calculator Materiale Termoizolatie application is a desktop solution designed to streamline the process of calculating thermal insulation material requirements for construction projects. It allows RED Construct to manage clients, building projects (obiective), materials, calculate consumption, and generate orders with detailed pricing.

---

## 2. FUNCTIONAL REQUIREMENTS

### 2.1 Client Management (Gestiunea Clientilor)

**FR-1.1: Add Client**
- System shall allow users to add new clients with the following information:
  - Company Name (Nume)
  - CUI (Tax Registration Number)
  - Address (Adresa)
  - Phone (Telefon)
  - Email
  - City/Locality (Localitate)
  - Postal Code (CodPostal)
- System shall validate required fields
- System shall prevent duplicate CUI entries

**FR-1.2: View Clients**
- System shall display all registered clients in a paginated list
- System shall show key client information

**FR-1.3: Edit Client**
- System shall allow modification of client information
- System shall update client data in the database

**FR-1.4: Delete Client**
- System shall allow deletion of clients with confirmation
- System shall prevent deletion if client has active projects

**FR-1.5: Search Clients**
- System shall support search by:
  - Company name
  - CUI
  - City/Locality
  - Email

---

### 2.2 Project Management (Gestiunea Obiectivelor)

**FR-2.1: Add Project**
- System shall allow users to add new construction projects with:
  - Project Name (Denumire)
  - Associated Client
  - Surface Area in m² (SuprafataM2)
  - Description (Descriere)
  - Address
  - City/Locality
  - Status (Active, Closed, Suspended)
- System shall validate surface area as positive decimal

**FR-2.2: View Projects**
- System shall display all projects with filters:
  - By Client
  - By Status
  - By Date Range

**FR-2.3: Edit Project**
- System shall allow modification of project details
- System shall update finalization date if applicable

**FR-2.4: Delete Project**
- System shall allow deletion with confirmation
- System shall prevent deletion if project has active calculations

**FR-2.5: View Project Details**
- System shall display:
  - All associated calculations
  - Total consumption per material
  - Associated orders

---

### 2.3 Material Management (Gestiunea Materialelor)

**FR-3.1: Add Material**
- System shall allow users to add thermal insulation materials with:
  - Name (Denumire)
  - Type (Tip): Polistiren, Adeziv, Dibluri, Plasa, Tencuiala, Amorsa
  - Description
  - Unit Price (Pret)
  - Unit (buc, kg, l, m², etc.)
  - Density (DensitateKgM3)
  - Thermal Conductivity (ConductivitateTermica) - W/(m·K)
  - Available Stock (StocDisponibil)

**FR-3.2: View Materials**
- System shall display materials sorted by:
  - Type
  - Name
  - Availability
- System shall show thermal properties

**FR-3.3: Edit Material**
- System shall allow modification of material properties and prices

**FR-3.4: Delete Material**
- System shall allow deletion with confirmation
- System shall prevent deletion if material is in use

**FR-3.5: Seed Default Materials**
- System shall pre-load common thermal insulation materials:
  - Expanded Polystyrene (XPS) 100mm, 80mm
  - Polystyrene Adhesive 25kg
  - Plastic dowels for polystyrene
  - Fiberglass mesh
  - Mineral plaster 25kg
  - Adhesion primer 10l

---

### 2.4 Consumption Calculation (Calcul Consum)

**FR-4.1: Create Calculation**
- System shall allow users to:
  - Select a project (Obiectiv)
  - Select material
  - Specify thickness (Grosime) in mm (for polystyrene)
  - Input consumption per m² (ConsumPeM2)
  - Optionally specify application efficiency/yield (Randament)

**FR-4.2: Auto-Calculate Consumption**
- System shall automatically calculate:
  - Total Consumption = Surface Area × Consumption per m²
  - Total Price = Total Consumption × Unit Price
- System shall account for efficiency/yield if specified

**FR-4.3: Pre-defined Consumption Rates**
- System shall provide default consumption rates for standard materials:
  - Polystyrene 100mm: X kg/m²
  - Adhesive: 5 kg/m²
  - Dowels: 4 per m²
  - Mesh: 1.1 m² (with overlap)
  - Plaster: Y kg/m²
  - Primer: Z l/m²

**FR-4.4: Store Calculations**
- System shall save calculations with:
  - Date of calculation
  - All input parameters
  - Calculated results
  - Notes/Observations

**FR-4.5: View Calculation Details**
- System shall display:
  - Consumption summary per material
  - Total material needed
  - Total cost estimation
  - Ability to export as PDF

**FR-4.6: Export Calculation**
- System shall generate PDF report with:
  - Client details
  - Project details
  - Material consumption table
  - Total costs
  - Timestamp

---

### 2.5 Order Management (Gestiunea Comenzilor)

**FR-5.1: Create Order**
- System shall allow users to:
  - Create new order (Comanda)
  - Associate with client and optional project
  - Set order date (auto-populated with current date)
  - Set delivery date
  - Initialize with status "Noua" (New)

**FR-5.2: Add Order Items**
- System shall allow adding materials to order (DetaliiComanda):
  - Select material
  - Specify quantity
  - Auto-populate unit price
  - Calculate line total
  - Optional: Apply line-level discount

**FR-5.3: Bulk Add from Calculation**
- System shall allow quick order creation by:
  - Selecting a previous calculation
  - Importing materials and quantities from calculation

**FR-5.4: Apply Pricing Modifiers**
- System shall support:
  - Overall order discount (percentage or fixed)
  - VAT calculation (default 19%)
  - Automatic total calculation

**FR-5.5: Order Status Management**
- System shall support status transitions:
  - Noua → Confirmata → In Preparare → Livrata
  - Noua → Anulata (any time)

**FR-5.6: View Order Details**
- System shall display:
  - Client information
  - Order items with prices
  - Discount summary
  - VAT breakdown
  - Total value

**FR-5.7: Print Order**
- System shall generate printable order invoice with:
  - Order number and date
  - Client details
  - Project (if associated)
  - Itemized materials
  - Pricing breakdown
  - Delivery date

**FR-5.8: Export Order**
- System shall generate PDF with professional formatting:
  - RED Construct branding
  - All order details
  - Signature field for customer
  - Payment terms (if configured)

---

### 2.6 Reporting (Rapoarte)

**FR-6.1: Orders by Client Report**
- System shall generate report showing:
  - Client list with total order values
  - Number of orders per client
  - Date range filter
  - Total revenue per client

**FR-6.2: Material Consumption Report**
- System shall generate report showing:
  - Material usage across all projects
  - Total quantities needed
  - Total costs per material
  - Comparison with available stock

**FR-6.3: Revenue Report**
- System shall generate report showing:
  - Total revenue by date range
  - Revenue by client
  - Revenue by project
  - Outstanding orders (not yet delivered)

---

### 2.7 Application Settings (Parametri Aplicatie)

**FR-7.1: VAT Configuration**
- System shall allow configuration of VAT percentage (default 19%)

**FR-7.2: Default Consumption Rates**
- System shall allow customization of default consumption rates for each material type

**FR-7.3: User Management**
- System shall support user accounts with roles (Admin, Operator, Viewer)
- System shall track user actions for audit purposes

---

## 3. NON-FUNCTIONAL REQUIREMENTS

### 3.1 Performance

**NFR-1.1:** System shall load client list (100+ records) in < 1 second  
**NFR-1.2:** System shall calculate consumption for any material in < 500ms  
**NFR-1.3:** System shall generate PDF reports in < 3 seconds  
**NFR-1.4:** System shall handle concurrent database operations without data corruption

### 3.2 Usability

**NFR-2.1:** Application shall have intuitive WPF UI with Material Design principles  
**NFR-2.2:** All numeric fields shall support both dot and comma as decimal separator  
**NFR-2.3:** System shall provide real-time calculation feedback (no manual refresh)  
**NFR-2.4:** System shall implement keyboard navigation shortcuts  
**NFR-2.5:** System shall provide helpful error messages in Romanian language

### 3.3 Reliability

**NFR-3.1:** System shall maintain data integrity with ACID-compliant transactions  
**NFR-3.2:** System shall validate all user input before database operations  
**NFR-3.3:** System shall provide automatic backup functionality  
**NFR-3.4:** System shall handle database connection failures gracefully with retry logic

### 3.4 Security

**NFR-4.1:** System shall use Windows Authentication for user login  
**NFR-4.2:** System shall encrypt sensitive data (client contacts, financial data)  
**NFR-4.3:** System shall enforce role-based access control  
**NFR-4.4:** System shall maintain audit logs of all modifications

### 3.5 Maintainability

**NFR-5.1:** Code shall follow MVVM pattern strictly  
**NFR-5.2:** Code shall include comprehensive XML documentation  
**NFR-5.3:** Code shall follow C# naming conventions (PascalCase for classes, camelCase for fields)  
**NFR-5.4:** Project structure shall separate concerns clearly (Models, Views, ViewModels, Data, Helpers)

### 3.6 Compatibility

**NFR-6.1:** Application shall run on Windows 10 and Windows 11  
**NFR-6.2:** Application shall support .NET 8 runtime  
**NFR-6.3:** Database shall be compatible with SQL Server 2019+ or LocalDB

### 3.7 Localization

**NFR-7.1:** UI shall support Romanian language (ro-RO)  
**NFR-7.2:** Numeric formatting shall follow Romanian conventions  
**NFR-7.3:** Date formatting shall follow Romanian conventions (dd/MM/yyyy)

---

## 4. DATABASE ENTITIES

### 4.1 Client (CLIENTI)
- **IdClient** (PK): Integer
- **Nume**: String, Required, Max 100
- **CUI**: String, Required, Max 50, Unique
- **Adresa**: String, Max 100
- **Telefon**: String, Max 20
- **Email**: String, Max 100
- **Localitate**: String, Max 50
- **CodPostal**: String, Max 10
- **DataInregistrare**: DateTime

### 4.2 Project (OBIECTIV)
- **IdObiectiv** (PK): Integer
- **Denumire**: String, Required, Max 150
- **IdClient** (FK): Integer
- **SuprafataM2**: Decimal, Required, > 0
- **Descriere**: String, Max 200
- **Adresa**: String, Max 100
- **Localitate**: String, Max 50
- **DataCrearii**: DateTime
- **DataFinalizarii**: DateTime, Nullable
- **Status**: String (Active, Closed, Suspended)

### 4.3 Material (MATERIALE)
- **IdMaterial** (PK): Integer
- **Denumire**: String, Required, Max 100
- **Tip**: String, Required (Polistiren, Adeziv, Dibluri, Plasa, Tencuiala, Amorsa)
- **Descriere**: String, Max 200
- **Pret**: Decimal, Required
- **Unitate**: String, Required (buc, kg, l, m², etc.)
- **DensitateKgM3**: Decimal, Required
- **ConductivitateTermica**: Decimal, Required
- **StocDisponibil**: Integer
- **DataAdaugarii**: DateTime
- **Activ**: Boolean

### 4.4 Consumption Calculation (CALCUL_CONSUM)
- **IdCalculConsum** (PK): Integer
- **IdObiectiv** (FK): Integer
- **IdMaterial** (FK): Integer
- **ConsumPeM2**: Decimal, Required
- **ConsumTotal**: Decimal, Required
- **PretUnitar**: Decimal, Required
- **PretTotal**: Decimal
- **Grosime**: Integer, Nullable (mm)
- **Randament**: Decimal, Nullable (%)
- **DataCalcul**: DateTime
- **Observatii**: String, Max 200

### 4.5 Order (COMENZI)
- **IdComanda** (PK): Integer
- **IdClient** (FK): Integer
- **IdObiectiv** (FK): Integer, Nullable
- **DataComanda**: DateTime
- **DataLivrare**: DateTime, Nullable
- **Status**: String (Noua, Confirmata, In Preparare, Livrata, Anulata)
- **ValoareTotala**: Decimal
- **TVA**: Decimal, Nullable
- **Reducere**: Decimal, Nullable
- **Observatii**: String, Max 300

### 4.6 Order Details (DETALII_COMANDA)
- **IdDetaliiComanda** (PK): Integer
- **IdComanda** (FK): Integer
- **IdMaterial** (FK): Integer
- **Cantitate**: Decimal, Required
- **PretUnitar**: Decimal, Required
- **PretTotal**: Decimal
- **ProcentReducere**: Decimal, Nullable

---

## 5. RELATIONSHIPS

- **Client → Obiective**: One-to-Many (1 client has many projects)
- **Client → Comenzi**: One-to-Many (1 client places many orders)
- **Obiectiv → CalculConsum**: One-to-Many (1 project has many calculations)
- **Obiectiv → Comenzi**: One-to-Many (1 project has many orders)
- **Material → CalculConsum**: One-to-Many (1 material in many calculations)
- **Material → DetaliiComanda**: One-to-Many (1 material in many order details)
- **Comanda → DetaliiComanda**: One-to-Many (1 order has many line items)

---

## 6. CONSTRAINTS & BUSINESS RULES

**BR-1:** Client CUI must be unique  
**BR-2:** Project surface area must be greater than 0  
**BR-3:** Consumption per m² must be positive  
**BR-4:** Material prices must be >= 0  
**BR-5:** Order cannot be deleted if status is "Livrata"  
**BR-6:** Cannot delete a client with active projects  
**BR-7:** Default VAT is 19% (configurable)  
**BR-8:** Consumption calculations must reference valid project and material  
**BR-9:** Order total = SUM(DetaliiComanda.PretTotal) + TVA - Reducere  
**BR-10:** Application timestamp should be in Romanian timezone (UTC+2/UTC+3)

---

## 7. USER ROLES & PERMISSIONS

### 7.1 Admin
- Full access to all features
- User management
- Application settings
- Data export/import

### 7.2 Operator
- Client management
- Project management
- Material management
- Calculation creation
- Order creation and management
- Report generation
- Cannot delete users or change system settings

### 7.3 Viewer
- View-only access to:
  - Client list
  - Projects
  - Calculations
  - Orders
- Can generate reports
- Cannot create or modify data

---

## 8. IMPLEMENTATION GUIDELINES

### 8.1 Technology Stack
- **Framework**: .NET 8
- **UI**: WPF (Windows Presentation Foundation)
- **ORM**: Entity Framework Core 8
- **Database**: SQL Server 2019+ or LocalDB
- **Architecture**: MVVM
- **Package Manager**: NuGet

### 8.2 Project Structure
```
CalculatorMateriale/
├── Models/              (Data entities)
├── Data/                (DbContext, Repositories, UnitOfWork)
├── Views/               (XAML files)
├── ViewModels/          (MVVM logic)
├── Helpers/             (Utilities, Converters, Helpers)
├── Resources/           (Styles, Templates)
├── Documentation/       (Diagrams, Requirements)
└── App.xaml             (Application configuration)
```

### 8.3 Code Standards
- Use async/await for database operations
- Implement INotifyPropertyChanged in ViewModels
- Use ICommand for user interactions
- Implement validation in Models using Data Annotations
- Use dependency injection for services

---

## 9. TESTING REQUIREMENTS

- Unit tests for Material Calculator logic
- Integration tests for database operations
- UI tests for critical workflows
- User acceptance testing (UAT) with RED Construct team

---

## 10. FUTURE ENHANCEMENTS

- Multi-language support (English, French)
- Mobile companion app
- Cloud synchronization
- Advanced reporting with charts
- Integration with accounting software
- Supplier management
- Quote generation
- Project timeline management
- Cost estimation accuracy tracking

---

## END OF DOCUMENT

Version: 1.0  
Last Updated: May 21, 2026  
Next Review: June 21, 2026
