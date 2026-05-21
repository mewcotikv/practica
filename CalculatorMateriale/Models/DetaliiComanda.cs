using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculatorMateriale.Models
{
    public class DetaliiComanda
    {
        [Key]
        public int IdDetaliiComanda { get; set; }

        [Required]
        public int IdComanda { get; set; }

        [Required]
        public int IdMaterial { get; set; }

        [Required]
        public decimal Cantitate { get; set; }

        [Required]
        public decimal PretUnitar { get; set; }

        public decimal PretTotal { get; set; } // Cantitate * PretUnitar

        public decimal? ProcentReducere { get; set; }

        // Foreign keys
        [ForeignKey(nameof(IdComanda))]
        public Comanda Comanda { get; set; }

        [ForeignKey(nameof(IdMaterial))]
        public Material Material { get; set; }
    }
}
