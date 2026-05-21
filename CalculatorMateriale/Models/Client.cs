using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CalculatorMateriale.Models
{
    public class Client
    {
        [Key]
        public int IdClient { get; set; }

        [Required]
        [StringLength(100)]
        public string Nume { get; set; }

        [Required]
        [StringLength(50)]
        public string CUI { get; set; }

        [StringLength(100)]
        public string Adresa { get; set; }

        [StringLength(20)]
        public string Telefon { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Localitate { get; set; }

        [StringLength(10)]
        public string CodPostal { get; set; }

        public DateTime DataInregistrare { get; set; } = DateTime.Now;

        // Navigation properties
        public ICollection<Obiectiv> Obiective { get; set; } = new List<Obiectiv>();
        public ICollection<Comanda> Comenzi { get; set; } = new List<Comanda>();
    }
}
