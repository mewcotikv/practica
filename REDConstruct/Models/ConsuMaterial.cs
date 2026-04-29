using System;

namespace REDConstruct.Models
{
    public class ConsuMaterial
    {
        public int Id { get; set; }
        public string Obiectiv { get; set; }
        public string Material { get; set; }
        public decimal Cantitate { get; set; }
        public string Unitate { get; set; }
        public decimal Cost { get; set; }
        public DateTime DataCalcul { get; set; }
    }
}
