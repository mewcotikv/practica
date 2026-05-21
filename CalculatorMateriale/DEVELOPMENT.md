# Development Notes

## Project Overview
**Calculator Materiale Termoizolatie** - RED Construct Thermal Insulation Materials Calculator
- **Platform:** WPF .NET 8
- **Pattern:** MVVM
- **Database:** SQL Server / LocalDB
- **Created:** May 21, 2026

---

## Architecture Decisions

### 1. MVVM Pattern
**Why:** Separates UI from business logic, enables testability and designer support
- **Models:** Data entities with validation
- **Views:** XAML UI (WPF)
- **ViewModels:** Business logic and commands
- **Data:** Repositories and services

### 2. Entity Framework Core
**Why:** Modern ORM for .NET, supports LINQ, migrations, and async operations
- Generic Repository pattern for code reuse
- Unit of Work pattern to coordinate transactions
- Fluent API for configuration
- Shadow properties for audit fields

### 3. Async/Await
**Why:** Responsive UI, efficient database operations
- All database calls are async
- Long operations run on thread pool
- UI thread remains responsive

### 4. Dependency Injection
**Why:** Loose coupling, testability, maintainability
- Container managed by Microsoft.Extensions.DependencyInjection
- Configuration in App.xaml.cs
- Services injected into ViewModels

---

## Material Types & Calculations

### Supported Materials
1. **Polistiren (Expanded Polystyrene)**
   - Types: XPS 100mm, 80mm
   - Unit: buc (piece)
   - Density: 35 kg/m³
   - Thermal conductivity: 0.029 W/m·K
   - Consumption: 1 piece/m² (100mm = ~4.5 kg/m²)

2. **Adeziv (Adhesive)**
   - Unit: sac (bag - 25kg)
   - Consumption: 5 kg/m² (~0.2 bags/m²)

3. **Dibluri (Dowels)**
   - Unit: buc (piece)
   - Consumption: 4 pieces/m²

4. **Plasa (Mesh)**
   - Unit: mp (m²)
   - Consumption: 1.1 m²/m² (10% overlap)

5. **Tencuiala (Plaster)**
   - Unit: sac (bag - 25kg)
   - Consumption: ~7 kg/m² (~0.28 bags/m²)

6. **Amorsa (Primer)**
   - Unit: galeata (bucket - 10l)
   - Consumption: 0.5 l/m²

---

## Database Design

### Entities
- **Client:** Stores client/company information
- **Obiectiv:** Building projects with surface area (m²)
- **Material:** Thermal properties, pricing
- **CalculConsum:** Consumption calculations per material per project
- **Comanda:** Orders with status tracking
- **DetaliiComanda:** Order line items

### Relationships
- One client → Many projects (1:M)
- One project → Many calculations (1:M)
- One project → Many orders (1:M)
- One material → Many calculations (1:M)
- One order → Many line items (1:M)

### Delete Behavior
- Client deletion restricted if has projects
- Project deletion cascades calculations
- Material deletion restricted if used in calculations
- Order deletion cascades line items

---

## Key Features Implementation

### 1. Material Consumption Calculation
```csharp
ConsumTotal = SuprafataM2 × ConsumPeM2
PretTotal = ConsumTotal × PretUnitar
```

### 2. VAT Calculation
```csharp
PretCuTVA = PretTotal × (1 + TVA/100)  // Default 19%
```

### 3. Order Total
```csharp
ValoareTotala = SUM(DetaliiComanda.PretTotal) + TVA - Reducere
```

---

## ViewModels Architecture

### Base Class: ViewModelBase
- Implements INotifyPropertyChanged
- Provides SetProperty helper for two-way binding
- OnPropertyChanged method for notifications

### RelayCommand
- ICommand implementation
- Generic and non-generic versions
- Supports CanExecute predicate

### Specific ViewModels
1. **ClientViewModel**
   - LoadClientiCommand
   - AddClientCommand
   - EditClientCommand
   - DeleteClientCommand
   - SearchCommand

2. **MaterialViewModel**
   - LoadMaterialeCommand
   - AddMaterialCommand
   - EditMaterialCommand
   - DeleteMaterialCommand

3. **ComandaViewModel**
   - LoadComenziCommand
   - AddComandaCommand
   - EditComandaCommand
   - DeleteComandaCommand
   - PrintComandaCommand

---

## Data Access Layer

### Repository Pattern
- Generic `IRepository<T>` interface
- `Repository<T>` implementation
- Standard CRUD operations
- Async/await support

### Unit of Work
- Coordinates multiple repositories
- Manages transaction scope
- Single SaveChanges() call
- Ensures data consistency

---

## Configuration

### Connection String
```
Server=(localdb)\mssqllocaldb;Database=CalculatorMateriale;Trusted_Connection=true;
```

### Application Settings
- Default VAT: 19%
- Currency: RON (Romanian Lei)
- Date Format: dd/MM/yyyy
- Decimal Separator: , (comma)

---

## Code Standards

### Naming Conventions
- Classes: PascalCase (e.g., `ClientViewModel`)
- Methods: PascalCase (e.g., `LoadClienti()`)
- Properties: PascalCase (e.g., `SelectedClient`)
- Fields: camelCase with underscore prefix (e.g., `_clienti`)
- Constants: UPPER_CASE (e.g., `DEFAULT_VAT`)

### XML Documentation
- Public members must have XML comments
- Include summary, remarks, examples where applicable
- Use `<param>`, `<returns>`, `<exception>` tags

### File Organization
- One class per file (except nested types)
- File name matches class name
- Logical grouping by namespace

---

## Testing Strategy

### Unit Tests
- Test MaterialCalculator methods
- Test validation logic
- Mock repositories

### Integration Tests
- Test database operations
- Test Unit of Work
- Test migrations

### UI Tests
- Test MVVM bindings
- Test command execution
- Test ViewModel logic

---

## Performance Considerations

### Database
- Use include() for related entities
- Lazy loading disabled
- Explicit eager loading with Include()
- Connection pooling enabled

### UI
- Virtual lists for large datasets
- Async operations don't block UI
- Efficient data binding
- Minimize property change notifications

### Caching
- Consider caching material list
- Cache calculation results
- Invalidate on data changes

---

## Future Enhancements

### Phase 2
- WPF UI with Material Design
- PDF export with iText7
- Professional styling
- Reporting module

### Phase 3
- User authentication (Windows Auth)
- Role-based access control
- Audit logging
- Application settings management

### Phase 4
- Advanced search/filtering
- Cloud synchronization
- Multi-language support
- Mobile app

---

## Troubleshooting

### Common Issues

**Issue:** LocalDB not found
- **Solution:** Install SQL Server Express LocalDB from Visual Studio installer

**Issue:** EF migrations fail
- **Solution:** Ensure database connection string is valid, run `dotnet ef database update`

**Issue:** NuGet package conflicts
- **Solution:** Run `dotnet restore`, check .csproj for conflicting versions

---

## Resources

- [Entity Framework Core Documentation](https://learn.microsoft.com/en-us/ef/core/)
- [MVVM Pattern](https://learn.microsoft.com/en-us/windows/uwp/data-and-cloud/mvvm)
- [WPF Data Binding](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/data-binding-overview)
- [C# Best Practices](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)

---

**Last Updated:** May 21, 2026  
**Team:** RED Construct Development
