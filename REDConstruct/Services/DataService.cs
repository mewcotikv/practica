using System;
using System.Collections.Generic;
using System.Linq;
using REDConstruct.Models;

namespace REDConstruct.Services
{
    public class DataService
    {
        // Mock data - in production, this would connect to a database
        private static List<Obiectiv> _obiective = new List<Obiectiv>
        {
            new Obiectiv { Id = 1, Nume = "Obiectiv A", Descriere = "Construcție Bloc A" },
            new Obiectiv { Id = 2, Nume = "Obiectiv B", Descriere = "Construcție Bloc B" },
            new Obiectiv { Id = 3, Nume = "Obiectiv C", Descriere = "Construcție Infrastructură" }
        };

        private static List<ConsuMaterial> _consumMateriale = new List<ConsuMaterial>
        {
            new ConsuMaterial { Id = 1, Obiectiv = "Obiectiv A", Material = "Ciment", Cantitate = 100, Unitate = "kg", Cost = 500, DataCalcul = new DateTime(2026, 4, 15) },
            new ConsuMaterial { Id = 2, Obiectiv = "Obiectiv A", Material = "Nisip", Cantitate = 50, Unitate = "m³", Cost = 300, DataCalcul = new DateTime(2026, 4, 16) },
            new ConsuMaterial { Id = 3, Obiectiv = "Obiectiv B", Material = "Ciment", Cantitate = 150, Unitate = "kg", Cost = 750, DataCalcul = new DateTime(2026, 4, 17) },
            new ConsuMaterial { Id = 4, Obiectiv = "Obiectiv B", Material = "Oțel", Cantitate = 30, Unitate = "t", Cost = 1500, DataCalcul = new DateTime(2026, 4, 18) },
            new ConsuMaterial { Id = 5, Obiectiv = "Obiectiv C", Material = "Pietre", Cantitate = 200, Unitate = "m³", Cost = 1000, DataCalcul = new DateTime(2026, 4, 19) },
            new ConsuMaterial { Id = 6, Obiectiv = "Obiectiv C", Material = "Nisip", Cantitate = 100, Unitate = "m³", Cost = 600, DataCalcul = new DateTime(2026, 4, 20) }
        };

        public List<Obiectiv> GetObiective()
        {
            return _obiective;
        }

        public List<RaportConsum> GetRaportConsum(DateTime dataStart, DateTime dataEnd, string obiectiv = null)
        {
            var filtered = _consumMateriale
                .Where(x => x.DataCalcul >= dataStart && x.DataCalcul <= dataEnd)
                .ToList();

            if (!string.IsNullOrEmpty(obiectiv) && obiectiv != "Toate")
            {
                filtered = filtered.Where(x => x.Obiectiv == obiectiv).ToList();
            }

            return filtered.Select(x => new RaportConsum
            {
                Obiectiv = x.Obiectiv,
                Material = x.Material,
                Cantitate = x.Cantitate,
                Cost = x.Cost,
                DataCalcul = x.DataCalcul
            }).ToList();
        }
    }
}
