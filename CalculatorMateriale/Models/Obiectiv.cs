using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculatorMateriale.Models
{
    public class Obiectiv
    {
        [Key]
        public int IdObiectiv { get; set; }

        [Required]
        [StringLength(150)]
        public string Denumire { get; set; }

        [Required]
        public int IdClient { get; set; }

        [Required]
        public decimal SuprafataM2 { get; set; }

        [StringLength(200)]
        public string Descriere { get; set; }

        [StringLength(100)]
        public string Adresa { get; set; }

        [StringLength(50)]
        public string Localitate { get; set; }

        public DateTime DataCrearii { get; set; } = DateTime.Now;

        public DateTime? DataFinalizarii { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Activ"; // Activ, Inchis, Suspendat

        // Foreign key
        [ForeignKey(nameof(IdClient))]
        public Client Client { get; set; }

        // Navigation properties
        public ICollection<CalculConsum> CalculConsume { get; set; } = new List<CalculConsum>();
        public ICollection<Comanda> Comenzi { get; set; } = new List<Comanda>();
    }
}
