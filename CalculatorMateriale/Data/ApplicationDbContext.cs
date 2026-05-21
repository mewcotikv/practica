using Microsoft.EntityFrameworkCore;
using CalculatorMateriale.Models;

namespace CalculatorMateriale.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clienti { get; set; }
        public DbSet<Obiectiv> Obiective { get; set; }
        public DbSet<Material> Materiale { get; set; }
        public DbSet<CalculConsum> CalculConsume { get; set; }
        public DbSet<Comanda> Comenzi { get; set; }
        public DbSet<DetaliiComanda> DetaliiComenzi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Client - Obiectiv relationship (One-to-Many)
            modelBuilder.Entity<Obiectiv>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Obiective)
                .HasForeignKey(o => o.IdClient)
                .OnDelete(DeleteBehavior.Restrict);

            // Client - Comanda relationship (One-to-Many)
            modelBuilder.Entity<Comanda>()
                .HasOne(c => c.Client)
                .WithMany(cl => cl.Comenzi)
                .HasForeignKey(c => c.IdClient)
                .OnDelete(DeleteBehavior.Restrict);

            // Obiectiv - Comanda relationship (One-to-Many)
            modelBuilder.Entity<Comanda>()
                .HasOne(c => c.Obiectiv)
                .WithMany(o => o.Comenzi)
                .HasForeignKey(c => c.IdObiectiv)
                .OnDelete(DeleteBehavior.SetNull);

            // Obiectiv - CalculConsum relationship (One-to-Many)
            modelBuilder.Entity<CalculConsum>()
                .HasOne(cc => cc.Obiectiv)
                .WithMany(o => o.CalculConsume)
                .HasForeignKey(cc => cc.IdObiectiv)
                .OnDelete(DeleteBehavior.Cascade);

            // Material - CalculConsum relationship (One-to-Many)
            modelBuilder.Entity<CalculConsum>()
                .HasOne(cc => cc.Material)
                .WithMany(m => m.CalculConsume)
                .HasForeignKey(cc => cc.IdMaterial)
                .OnDelete(DeleteBehavior.Restrict);

            // Comanda - DetaliiComanda relationship (One-to-Many)
            modelBuilder.Entity<DetaliiComanda>()
                .HasOne(dc => dc.Comanda)
                .WithMany(c => c.DetaliiComenzi)
                .HasForeignKey(dc => dc.IdComanda)
                .OnDelete(DeleteBehavior.Cascade);

            // Material - DetaliiComanda relationship (One-to-Many)
            modelBuilder.Entity<DetaliiComanda>()
                .HasOne(dc => dc.Material)
                .WithMany(m => m.DetaliiComenzi)
                .HasForeignKey(dc => dc.IdMaterial)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed default materials
            modelBuilder.Entity<Material>().HasData(
                new Material { IdMaterial = 1, Denumire = "Polistiren Expandat XPS 100mm", Tip = "Polistiren", Pret = 45.50m, Unitate = "buc", DensitateKgM3 = 35, ConductivitateTermica = 0.029m },
                new Material { IdMaterial = 2, Denumire = "Polistiren Expandat XPS 80mm", Tip = "Polistiren", Pret = 36.00m, Unitate = "buc", DensitateKgM3 = 35, ConductivitateTermica = 0.029m },
                new Material { IdMaterial = 3, Denumire = "Adeziv pentru Polistiren 25kg", Tip = "Adeziv", Pret = 75.00m, Unitate = "sac", DensitateKgM3 = 1400, ConductivitateTermica = 0.7m },
                new Material { IdMaterial = 4, Denumire = "Dibluri plastic pentru polistiren", Tip = "Dibluri", Pret = 0.85m, Unitate = "buc", DensitateKgM3 = 1050, ConductivitateTermica = 0.4m },
                new Material { IdMaterial = 5, Denumire = "Plasa fibra sticla 5mm", Tip = "Plasa", Pret = 12.50m, Unitate = "mp", DensitateKgM3 = 160, ConductivitateTermica = 0.15m },
                new Material { IdMaterial = 6, Denumire = "Tencuiala Minerala 25kg", Tip = "Tencuiala", Pret = 55.00m, Unitate = "sac", DensitateKgM3 = 1800, ConductivitateTermica = 0.8m },
                new Material { IdMaterial = 7, Denumire = "Amorsa de aderenta 10l", Tip = "Amorsa", Pret = 68.00m, Unitate = "galeata", DensitateKgM3 = 1200, ConductivitateTermica = 0.5m }
            );
        }
    }
}
