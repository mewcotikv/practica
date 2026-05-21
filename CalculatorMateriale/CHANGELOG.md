# Changelog

All notable changes to the Calculator Materiale Termoizolatie project will be documented in this file.

## [1.0.0] - 2026-05-21

### Added
- Initial project setup and structure
- MVVM architecture implementation
- Entity models (Client, Obiectiv, Material, CalculConsum, Comanda, DetaliiComanda)
- Entity Framework Core data access layer
- Generic Repository and Unit of Work patterns
- MVVM helper classes (RelayCommand, ViewModelBase)
- Material Calculator utility class
- Initial ViewModels (ClientViewModel, MaterialViewModel, ComandaViewModel)
- Database configuration and seed data
- Project configuration files (appsettings.json)
- Comprehensive project documentation
  - Requirements document with functional/non-functional specs
  - Entity Relationship Diagram (Mermaid)
  - Use Case Diagram (PlantUML)
  - Project structure documentation

### In Progress
- WPF UI implementation (Views)
- Complete ViewModel implementations
- Additional ViewModels (MainViewModel, ObiectivViewModel, CalculConsumViewModel)

### Planned (Phase 2)
- [ ] XAML UI design
- [ ] Client management window
- [ ] Project management window
- [ ] Material management window
- [ ] Calculation creation interface
- [ ] Order management and creation
- [ ] PDF export functionality
- [ ] Reporting module

### Planned (Phase 3)
- [ ] User authentication
- [ ] Role-based access control
- [ ] Audit logging
- [ ] Application settings UI
- [ ] Data validation
- [ ] Error handling and user feedback

### Planned (Phase 4)
- [ ] Advanced search and filtering
- [ ] Batch import/export
- [ ] Performance optimization
- [ ] Multi-language support
- [ ] Mobile companion app

---

## Notes

### Database
- Uses LocalDB by default in development
- Configured for SQL Server 2019+
- Supports multiple active result sets (MARS)

### Architecture
- Follows MVVM pattern strictly
- Uses async/await throughout
- Implements dependency injection

### Standards
- C# 12 language features
- .NET 8 framework
- Entity Framework Core 8
- Nullable reference types enabled
