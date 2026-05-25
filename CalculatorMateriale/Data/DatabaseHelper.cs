using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CalculatorMateriale.Data
{
    /// <summary>
    /// Ajutător pentru gestionarea conexiunii și inițializarea bazei de date RedConstructDB
    /// </summary>
    public class DatabaseHelper
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DatabaseHelper> _logger;
        private readonly ApplicationDbContext _dbContext;

        public DatabaseHelper(
            IConfiguration configuration,
            ILogger<DatabaseHelper> logger,
            ApplicationDbContext dbContext)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Obține șirul de conexiune pentru RedConstructDB din appsettings.json
        /// </summary>
        public string GetConnectionString()
        {
            var connectionString = _configuration.GetConnectionString("RedConstructDB");

            if (string.IsNullOrEmpty(connectionString))
            {
                _logger.LogWarning("Șirul de conexiune 'RedConstructDB' nu a fost găsit în configurație.");
                // Fallback la SQL Server LocalDB implicit
                connectionString = @"Server=(localdb)\mssqllocaldb;Database=RedConstructDB;Trusted_Connection=true;";
                _logger.LogInformation($"Se folosește șirul de conexiune implicit: {connectionString}");
            }

            return connectionString;
        }

        /// <summary>
        /// Inițializează baza de date (creează dacă nu există)
        /// </summary>
        public async Task InitializeDatabaseAsync()
        {
            try
            {
                _logger.LogInformation("Se inițializează baza de date RedConstructDB...");

                // Verificare și creare bază de date dacă nu există
                var canConnect = await _dbContext.Database.CanConnectAsync();

                if (!canConnect)
                {
                    _logger.LogInformation("Baza de date nu existe, se creează...");
                    await _dbContext.Database.EnsureCreatedAsync();
                    _logger.LogInformation("Baza de date RedConstructDB a fost creată cu succes.");
                }
                else
                {
                    _logger.LogInformation("Baza de date RedConstructDB este disponibilă.");
                }

                // Aplică migrări dacă sunt necesare
                try
                {
                    await _dbContext.Database.MigrateAsync();
                    _logger.LogInformation("Migrările bazei de date au fost aplicate cu succes.");
                }
                catch (Exception migrationEx)
                {
                    _logger.LogWarning($"Nu s-au putut aplica migrări: {migrationEx.Message}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare la inițializarea bazei de date: {ex.Message}\n{ex.InnerException?.Message}");
                throw;
            }
        }

        /// <summary>
        /// Verifică conexiunea la baza de date
        /// </summary>
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                _logger.LogInformation("Se testează conexiunea la RedConstructDB...");
                var canConnect = await _dbContext.Database.CanConnectAsync();

                if (canConnect)
                {
                    _logger.LogInformation("Conexiunea la RedConstructDB a fost stabilită cu succes.");
                    return true;
                }
                else
                {
                    _logger.LogError("Nu se poate conecta la RedConstructDB.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare la testarea conexiunii: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Șterge și recreează baza de date (ATENȚIE: va pierde datele!)
        /// </summary>
        public async Task ResetDatabaseAsync()
        {
            try
            {
                _logger.LogWarning("Se șterge și se recreează baza de date RedConstructDB...");

                // Șterge baza de date
                await _dbContext.Database.EnsureDeletedAsync();
                _logger.LogInformation("Baza de date a fost ștearsă.");

                // Recrează baza de date
                await _dbContext.Database.EnsureCreatedAsync();
                _logger.LogInformation("Baza de date RedConstructDB a fost recreată.");

                // Aplică migrări
                await _dbContext.Database.MigrateAsync();
                _logger.LogInformation("Migrările au fost aplicate.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare la resetarea bazei de date: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obține informații despre conexiune la baza de date
        /// </summary>
        public DatabaseInfo GetDatabaseInfo()
        {
            try
            {
                var connection = _dbContext.Database.GetDbConnection();
                return new DatabaseInfo
                {
                    Server = connection.DataSource,
                    Database = connection.Database,
                    State = connection.State.ToString(),
                    ConnectionString = GetConnectionString()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare la obținerea informațiilor bazei de date: {ex.Message}");
                return new DatabaseInfo { Error = ex.Message };
            }
        }
    }

    /// <summary>
    /// Model pentru informații despre baza de date
    /// </summary>
    public class DatabaseInfo
    {
        public string? Server { get; set; }
        public string? Database { get; set; }
        public string? State { get; set; }
        public string? ConnectionString { get; set; }
        public string? Error { get; set; }
    }
}
