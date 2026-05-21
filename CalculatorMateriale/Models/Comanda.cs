using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculatorMateriale.Models
{
    public class Comanda
    {
        [Key]
        public int IdComanda { get; set; }

        [Required]
        public int IdClient { get; set; }

        public int? IdObiectiv { get; set; }

        [Required]
        public DateTime DataComanda { get; set; } = DateTime.Now;

        public DateTime? DataLivrare { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Noua"; // Noua, Confirmata, In Preparare, Livrata, Anulata

        public decimal ValoareTotala { get; set; }

        public decimal? TVA { get; set; }

        public decimal? Reducere { get; set; }

        [StringLength(300)]
        public string Observatii { get; set; }

        // Foreign keys
        [ForeignKey(nameof(IdClient))]
        public Client Client { get; set; }

        [ForeignKey(nameof(IdObiectiv))]
        public Obiectiv Obiectiv { get; set; }

        // Navigation properties
        public ICollection<DetaliiComanda> DetaliiComenzi { get; set; } = new List<DetaliiComanda>();
    }
}
