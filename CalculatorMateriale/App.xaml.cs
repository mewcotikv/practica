using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CalculatorMateriale.Data;
using CalculatorMateriale.Models;
using CalculatorMateriale.ViewModels;
using System.Linq;

namespace CalculatorMateriale
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider? _serviceProvider;
        private IConfiguration? _configuration;

        public App()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += (_, args) =>
                WriteCrashLog(args.ExceptionObject as Exception);
            DispatcherUnhandledException += (_, args) =>
            {
                WriteCrashLog(args.Exception);
                MessageBox.Show($"Eroare aplicatie: {args.Exception.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                args.Handled = true;
            };
            
            // Configurare aplicație
            ConfigureServices();
        }

        private static void WriteCrashLog(Exception? exception)
        {
            if (exception == null)
                return;

            try
            {
                System.IO.File.AppendAllText(
                    System.IO.Path.Combine(AppContext.BaseDirectory, "crash.log"),
                    $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\n{exception}\n\n");
            }
            catch
            {
            }
        }

        private void ConfigureServices()
        {
            try
            {
                // Build configuration
                var configBuilder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile("appsettings.Development.json", optional: true);

                _configuration = configBuilder.Build();

                // Setup DependencyInjection
                var services = new ServiceCollection();

                // Add configuration
                services.AddSingleton(_configuration);

                // Add logging - Simplified without external packages
                services.AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(LogLevel.Information);
                });

                // Add DbContext - local SQLite database
                var connectionString = _configuration.GetConnectionString("RedConstructDB")
                    ?? "Data Source=RedConstructDB_Dev.db";
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(connectionString), ServiceLifetime.Transient);

                // Add repositories and services
                services.AddTransient<IUnitOfWork, UnitOfWork>();
                services.AddTransient<DatabaseHelper>();

                // Add ViewModels
                services.AddTransient<MainWindow>();
                services.AddTransient<ClientiViewModel>();
                services.AddTransient<CalculConsumViewModel>();
                services.AddTransient<ComandaViewModel>();
                services.AddTransient<MaterialViewModel>();
                services.AddTransient<ObiectivViewModel>();
                services.AddTransient<ClientViewModel>();

                // Build service provider
                _serviceProvider = services.BuildServiceProvider();

                // Store services in Application properties for access from UserControls
                var unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
                this.Properties["UnitOfWork"] = unitOfWork;

                // Initialize database (moved to OnStartup for SplashScreen)
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la inițializarea aplicației: {ex.Message}\n\n{ex.StackTrace}",
                    "Eroare Inițializare", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Shutdown();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);
                
                // Show SplashScreen
                var splash = new SplashScreen();
                splash.Show("Inițializare aplicație...");

                if (_serviceProvider != null)
                {
                    // Initialize database
                    splash.UpdateProgress(70, "Inițializare bază de date...");
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        dbContext.Database.EnsureCreated();
                        EnsureSqliteSchema(dbContext);
                        SeedDemoData(dbContext);
                    }

                    // Create MainWindow
                    splash.UpdateProgress(90, "Inițializare interfață...");
                    var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
                    mainWindow.MinWidth = 900;
                    mainWindow.MinHeight = 600;

                    splash.UpdateProgress(100, "Gata!");
                    splash.Close();
                    mainWindow.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la pornire: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Shutdown();
            }
        }

        private static void EnsureSqliteSchema(ApplicationDbContext dbContext)
        {
            if (!dbContext.Database.IsSqlite())
                return;

            var createScript = dbContext.Database.GenerateCreateScript();
            var statements = createScript.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (var statement in statements)
            {
                var sql = statement.Trim();
                if (string.IsNullOrWhiteSpace(sql))
                    continue;

                try
                {
                    dbContext.Database.ExecuteSqlRaw(sql);
                }
                catch (Exception ex) when (
                    ex.Message.Contains("already exists", StringComparison.OrdinalIgnoreCase) ||
                    ex.Message.Contains("UNIQUE constraint failed", StringComparison.OrdinalIgnoreCase))
                {
                    // Existing partial SQLite databases keep their data; missing tables continue to be created.
                }
            }
        }

        private static void SeedDemoData(ApplicationDbContext dbContext)
        {
            var clientSeeds = new[]
            {
                new Client
                {
                    Nume = "RED Construct SRL",
                    CUI = "1003600001111",
                    Localitate = "Chisinau",
                    Adresa = "str. Constructorilor 12",
                    Telefon = "+373 22 111 222",
                    Email = "office@redconstruct.md",
                    Activ = true
                },
                new Client
                {
                    Nume = "Casa Termica SRL",
                    CUI = "1003600002222",
                    Localitate = "Balti",
                    Adresa = "str. Independentei 45",
                    Telefon = "+373 231 55 444",
                    Email = "comenzi@casatermica.md",
                    Activ = true
                }
            };

            foreach (var client in clientSeeds)
            {
                if (!dbContext.Clienti.Any(c => c.CUI == client.CUI))
                    dbContext.Clienti.Add(client);
            }

            dbContext.SaveChanges();

            Material EnsureMaterial(string denumire, string tip, decimal pret, string unitate, int stoc, decimal densitate, decimal conductivitate, string descriere)
            {
                var material = dbContext.Materiale.FirstOrDefault(m => m.Denumire == denumire);
                if (material == null)
                {
                    material = new Material { Denumire = denumire };
                    dbContext.Materiale.Add(material);
                }

                material.Tip = tip;
                material.Pret = pret;
                material.Unitate = unitate;
                material.StocDisponibil = stoc;
                material.DensitateKgM3 = densitate;
                material.ConductivitateTermica = conductivitate;
                material.Descriere = descriere;
                material.Activ = true;

                return material;
            }

            EnsureMaterial("Adeziv plasa 160 gr - Caparol 100R", "Adeziv", 31m, "kg", 900, 1400m, 0.70m,
                "Adeziv/masa de armare pentru plasa fibra sticla 160 gr.");
            EnsureMaterial("Plasa fibra sticla 160 gr", "Plasa", 14.50m, "mp", 600, 160m, 0.15m,
                "Plasa de armare pentru fatada si sisteme termoizolante.");
            EnsureMaterial("Polistirol clasa M100 100mm", "Polistiren", 95m, "mp", 300, 20m, 0.038m,
                "Polistirol fatada clasa M100, grosime 100 mm.");
            EnsureMaterial("Polistirol clasa M90 100mm", "Polistiren", 88m, "mp", 280, 18m, 0.039m,
                "Polistirol fatada clasa M90, grosime 100 mm.");
            EnsureMaterial("Caparol ArmaReno 700", "Adeziv", 145m, "sac", 120, 1400m, 0.70m,
                "Mortar mineral universal pentru lipire, armare si renovare fatade.");
            EnsureMaterial("Caparol Fassadenputz K15 25kg", "Tencuiala", 720m, "galeata", 70, 1800m, 0.80m,
                "Tencuiala decorativa acrilica pentru fatade, structura K15.");
            EnsureMaterial("Caparol Buntsteinputz 25kg", "Tencuiala mozaicata", 980m, "galeata", 55, 1800m, 0.80m,
                "Tencuiala mozaicata pentru soclu, fatade si zone decorative.");
            EnsureMaterial("Caparol Putzgrund", "Grund", 285m, "galeata", 80, 1200m, 0.50m,
                "Grund pentru pregatirea stratului suport inainte de tencuieli decorative.");
            EnsureMaterial("CapaSol RapidGrund", "Amorsa", 260m, "bidon", 60, 1050m, 0.50m,
                "Amorsa pentru suporturi absorbante inainte de Putzgrund si tencuiala.");

            dbContext.SaveChanges();

            var redClient = dbContext.Clienti.FirstOrDefault(c => c.CUI == "1003600001111");
            var casaClient = dbContext.Clienti.FirstOrDefault(c => c.CUI == "1003600002222");

            if (redClient != null && !dbContext.Comenzi.Any(c => c.Observatii == "Demo RED fatada"))
            {
                dbContext.Comenzi.Add(new Comanda
                {
                    IdClient = redClient.IdClient,
                    DataComanda = DateTime.Now.Date,
                    DataLivrare = DateTime.Now.Date.AddDays(3),
                    Status = "Confirmata",
                    ValoareTotala = 18450m,
                    TVA = 3690m,
                    Observatii = "Demo RED fatada"
                });
            }

            if (casaClient != null && !dbContext.Comenzi.Any(c => c.Observatii == "Demo materiale soclu"))
            {
                dbContext.Comenzi.Add(new Comanda
                {
                    IdClient = casaClient.IdClient,
                    DataComanda = DateTime.Now.Date.AddDays(-1),
                    DataLivrare = DateTime.Now.Date.AddDays(5),
                    Status = "Noua",
                    ValoareTotala = 7320m,
                    TVA = 1464m,
                    Observatii = "Demo materiale soclu"
                });
            }

            if (redClient != null && !dbContext.Comenzi.Any(c => c.Observatii == "Pachet Caparol fatada"))
            {
                dbContext.Comenzi.Add(new Comanda
                {
                    IdClient = redClient.IdClient,
                    DataComanda = DateTime.Now.Date,
                    DataLivrare = DateTime.Now.Date.AddDays(7),
                    Status = "Noua",
                    ValoareTotala = 28660m,
                    TVA = 5732m,
                    Observatii = "Pachet Caparol fatada"
                });
            }

            dbContext.SaveChanges();

            void AddDetailIfMissing(Comanda? comanda, string denumireMaterial, decimal cantitate)
            {
                if (comanda == null || dbContext.DetaliiComenzi.Any(d => d.IdComanda == comanda.IdComanda))
                    return;

                var material = dbContext.Materiale.FirstOrDefault(m => m.Denumire == denumireMaterial);
                if (material == null)
                    return;

                dbContext.DetaliiComenzi.Add(new DetaliiComanda
                {
                    IdComanda = comanda.IdComanda,
                    IdMaterial = material.IdMaterial,
                    Cantitate = cantitate,
                    PretUnitar = material.Pret,
                    PretTotal = decimal.Round(cantitate * material.Pret, 2)
                });
            }

            AddDetailIfMissing(
                dbContext.Comenzi.FirstOrDefault(c => c.Observatii == "Demo RED fatada"),
                "Adeziv plasa 160 gr - Caparol 100R",
                250m);
            AddDetailIfMissing(
                dbContext.Comenzi.FirstOrDefault(c => c.Observatii == "Demo materiale soclu"),
                "Caparol Buntsteinputz 25kg",
                8m);
            AddDetailIfMissing(
                dbContext.Comenzi.FirstOrDefault(c => c.Observatii == "Pachet Caparol fatada"),
                "Caparol Fassadenputz K15 25kg",
                18m);
            dbContext.SaveChanges();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
