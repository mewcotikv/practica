using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CalculatorMateriale.Models
{
    /// <summary>
    /// Entitatea Material - Reprezintă materialele de termoizolație disponibile
    /// </summary>
    public class Material
    {
        /// <summary>
        /// Identificator unic al materialului (Primary Key)
        /// </summary>
        [Key]
        public int IdMaterial { get; set; }

        /// <summary>
        /// Denumirea materialului (de ex: "Polistiren 100mm", "Adeziv pentru EPS")
        /// </summary>
        [Required(ErrorMessage = "Denumirea materialului este obligatorie")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Lungimea trebuie să fie între 3 și 100 de caractere")]
        public string Denumire { get; set; } = string.Empty;

        /// <summary>
        /// Tipul materialului (Polistiren, Adeziv, Dibluri, Plasa, Tencuiala, Amorsa)
        /// </summary>
        [Required(ErrorMessage = "Tipul materialului este obligatoriu")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Lungimea trebuie să fie între 3 și 50 de caractere")]
        public string Tip { get; set; } = string.Empty;

        /// <summary>
        /// Descriere detaliată a materialului
        /// </summary>
        [StringLength(500)]
        public string? Descriere { get; set; }

        /// <summary>
        /// Prețul pe unitate de măsură (în MDL - lei moldoveni)
        /// </summary>
        [Required(ErrorMessage = "Prețul este obligatoriu")]
        [Range(0.01, 999999.99, ErrorMessage = "Prețul trebuie să fie între 0.01 și 999999.99")]
        public decimal Pret { get; set; }

        /// <summary>
        /// Unitatea de măsură (buc, kg, l, mp, m, etc.)
        /// </summary>
        [Required(ErrorMessage = "Unitatea de măsură este obligatorie")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Lungimea trebuie să fie între 1 și 20 de caractere")]
        public string Unitate { get; set; } = string.Empty;

        /// <summary>
        /// Densitate în kg/m³ (folosit pentru calcule)
        /// </summary>
        [Required(ErrorMessage = "Densitatea este obligatorie")]
        [Range(1, 10000, ErrorMessage = "Densitatea trebuie să fie între 1 și 10000")]
        public decimal DensitateKgM3 { get; set; }

        /// <summary>
        /// Conductivitate termică în W/(m·K) (proprietate izolantă)
        /// </summary>
        [Required(ErrorMessage = "Conductivitatea termică este obligatorie")]
        [Range(0.001, 100, ErrorMessage = "Conductivitatea termică trebuie să fie între 0.001 și 100")]
        public decimal ConductivitateTermica { get; set; }

        /// <summary>
        /// Stoc disponibil în depozit
        /// </summary>
        public int StocDisponibil { get; set; } = 0;

        /// <summary>
        /// Data adăugării materialului în sistem
        /// </summary>
        public DateTime DataAdaugarii { get; set; } = DateTime.Now;

        /// <summary>
        /// Stare: True = activ, False = retras din catalog
        /// </summary>
        public bool Activ { get; set; } = true;

        /// <summary>
        /// Navigare: Detaliile comenzilor care conțin acest material
        /// </summary>
        public virtual ICollection<DetaliiComanda> DetaliiComenzi { get; set; } = new List<DetaliiComanda>();

        /// <summary>
        /// Navigare: Calculele de consum pentru acest material
        /// </summary>
        public virtual ICollection<CalculConsum> CalculConsume { get; set; } = new List<CalculConsum>();

        /// <summary>
        /// Override pentru afișare ușoară
        /// </summary>
        public override string ToString()
        {
            return $"{Denumire} - {Pret} MDL/{Unitate}";
        }
    }
}
