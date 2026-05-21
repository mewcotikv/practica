using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculatorMateriale.Models
{
    public class CalculConsum
    {
        [Key]
        public int IdCalculConsum { get; set; }

        [Required]
        public int IdObiectiv { get; set; }

        [Required]
        public int IdMaterial { get; set; }

        [Required]
        public decimal ConsumPeM2 { get; set; } // Consumul materialului per m² (kg/m², buc/m², l/m², etc.)

        [Required]
        public decimal ConsumTotal { get; set; } // SuprafataM2 * ConsumPeM2

        [Required]
        public decimal PretUnitar { get; set; }

        public decimal PretTotal { get; set; } // ConsumTotal * PretUnitar

        public int? Grosime { get; set; } // in mm (pentru polistiren)

        public decimal? Randament { get; set; } // Procentul de randament al aplicării

        public DateTime DataCalcul { get; set; } = DateTime.Now;

        [StringLength(200)]
        public string Observatii { get; set; }

        // Foreign keys
        [ForeignKey(nameof(IdObiectiv))]
        public Obiectiv Obiectiv { get; set; }

        [ForeignKey(nameof(IdMaterial))]
        public Material Material { get; set; }
    }
}
