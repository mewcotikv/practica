using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CalculatorMateriale.Models
{
    /// <summary>
    /// Entitatea Client - Reprezintă clienții care fac comenzi de materiale termoizolante
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Identificator unic al clientului (Primary Key)
        /// </summary>
        [Key]
        public int IdClient { get; set; }

        /// <summary>
        /// Denumirea/Numele companiei clientului
        /// </summary>
        [Required(ErrorMessage = "Denumirea clientului este obligatorie")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Lungimea trebuie să fie între 3 și 100 de caractere")]
        public string Nume { get; set; } = string.Empty;

        /// <summary>
        /// Codul de Identificare Unică (CUI) al companiei
        /// </summary>
        [Required(ErrorMessage = "CUI este obligatoriu")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Lungimea CUI trebuie să fie între 6 și 50 de caractere")]
        [RegularExpression(@"^[0-9]{6,}$", ErrorMessage = "CUI trebuie să conțină doar cifre")]
        public string CUI { get; set; } = string.Empty;

        /// <summary>
        /// Adresa sediului social
        /// </summary>
        [StringLength(200)]
        public string? Adresa { get; set; }

        /// <summary>
        /// Telefonul de contact al clientului
        /// </summary>
        [StringLength(20)]
        [RegularExpression(@"^[+]?[0-9\s\-()]*$", ErrorMessage = "Formatul telefonului nu este valid")]
        public string? Telefon { get; set; }

        /// <summary>
        /// Adresa de email
        /// </summary>
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Adresa de email nu este validă")]
        public string? Email { get; set; }

        /// <summary>
        /// Localitatea/Orașul
        /// </summary>
        [StringLength(50)]
        public string? Localitate { get; set; }

        /// <summary>
        /// Codul poștal
        /// </summary>
        [StringLength(10)]
        public string? CodPostal { get; set; }

        /// <summary>
        /// Data înregistrării clientului în sistem
        /// </summary>
        public DateTime DataInregistrare { get; set; } = DateTime.Now;

        /// <summary>
        /// Stare: True = activ, False = inactiv/arhivat
        /// </summary>
        public bool Activ { get; set; } = true;

        /// <summary>
        /// Navigare: Proiectele/Obiectivele clientului
        /// </summary>
        public virtual ICollection<Obiectiv> Obiective { get; set; } = new List<Obiectiv>();

        /// <summary>
        /// Navigare: Comenzile clientului
        /// </summary>
        public virtual ICollection<Comanda> Comenzi { get; set; } = new List<Comanda>();

        /// <summary>
        /// Override pentru afișare ușoară
        /// </summary>
        public override string ToString()
        {
            return $"{Nume} (CUI: {CUI})";
        }
    }
}
