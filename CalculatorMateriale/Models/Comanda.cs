using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculatorMateriale.Models
{
    /// <summary>
    /// Entitatea Comanda - Reprezintă comenzile de materiale ale clienților
    /// </summary>
    public class Comanda
    {
        /// <summary>
        /// Identificator unic al comenzii (Primary Key)
        /// </summary>
        [Key]
        public int IdComanda { get; set; }

        /// <summary>
        /// Foreign Key - ID-ul clientului care a plasat comanda
        /// </summary>
        [Required(ErrorMessage = "Clientul este obligatoriu")]
        public int IdClient { get; set; }

        /// <summary>
        /// Foreign Key - ID-ul obiectivului/proiectului (opțional)
        /// </summary>
        public int? IdObiectiv { get; set; }

        /// <summary>
        /// Data plasării comenzii
        /// </summary>
        [Required(ErrorMessage = "Data comenzii este obligatorie")]
        public DateTime DataComanda { get; set; } = DateTime.Now;

        /// <summary>
        /// Data estimată/efectivă de livrare
        /// </summary>
        public DateTime? DataLivrare { get; set; }

        /// <summary>
        /// Starea comenzii (Noua, Confirmata, In Preparare, Livrata, Anulata)
        /// </summary>
        [Required(ErrorMessage = "Statusul comenzii este obligatoriu")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Lungimea trebuie să fie între 3 și 20 de caractere")]
        public string Status { get; set; } = "Noua";

        /// <summary>
        /// Valoarea totală a comenzii (în MDL)
        /// </summary>
        [Range(0, 9999999.99, ErrorMessage = "Valoarea trebuie să fie pozitivă")]
        public decimal ValoareTotala { get; set; }

        /// <summary>
        /// Procent TVA aplicat (19% standard în Moldova)
        /// </summary>
        [Range(0, 100, ErrorMessage = "TVA trebuie să fie între 0 și 100 la sută")]
        public decimal? TVA { get; set; }

        /// <summary>
        /// Reducere acordată (în MDL sau procent, depinde de implementare)
        /// </summary>
        [Range(0, 9999999.99, ErrorMessage = "Reducerea nu poate fi negativă")]
        public decimal? Reducere { get; set; }

        /// <summary>
        /// Observații/Note suplimentare privind comanda
        /// </summary>
        [StringLength(500)]
        public string? Observatii { get; set; }

        /// <summary>
        /// Referință către clientul care a plasat comanda
        /// </summary>
        [ForeignKey(nameof(IdClient))]
        public virtual Client? Client { get; set; }

        /// <summary>
        /// Referință către obiectivul/proiectul asociat (dacă există)
        /// </summary>
        [ForeignKey(nameof(IdObiectiv))]
        public virtual Obiectiv? Obiectiv { get; set; }

        /// <summary>
        /// Navigare: Detaliile comenzii (materialele și cantitățile)
        /// </summary>
        public virtual ICollection<DetaliiComanda> DetaliiComenzi { get; set; } = new List<DetaliiComanda>();

        /// <summary>
        /// Override pentru afișare ușoară
        /// </summary>
        public override string ToString()
        {
            return $"Comanda #{IdComanda} - {Status} ({DataComanda:dd/MM/yyyy})";
        }
    }
}
