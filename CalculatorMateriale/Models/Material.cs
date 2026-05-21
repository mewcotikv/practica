using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CalculatorMateriale.Models
{
    public class Material
    {
        [Key]
        public int IdMaterial { get; set; }

        [Required]
        [StringLength(100)]
        public string Denumire { get; set; }

        [Required]
        [StringLength(50)]
        public string Tip { get; set; } // Polistiren, Adeziv, Dibluri, Plasa, Tencuiala, Amorsa

        [StringLength(200)]
        public string Descriere { get; set; }

        [Required]
        public decimal Pret { get; set; } // Pret pe unitate

        [Required]
        [StringLength(20)]
        public string Unitate { get; set; } // buc, kg, l, mp, etc.

        [Required]
        public decimal DensitateKgM3 { get; set; }

        [Required]
        public decimal ConductivitateTermica { get; set; } // W/(m·K)

        public int StocDisponibil { get; set; } = 0;

        public DateTime DataAdaugarii { get; set; } = DateTime.Now;

        public bool Activ { get; set; } = true;

        // Navigation properties
        public ICollection<DetaliiComanda> DetaliiComenzi { get; set; } = new List<DetaliiComanda>();
        public ICollection<CalculConsum> CalculConsume { get; set; } = new List<CalculConsum>();
    }
}
