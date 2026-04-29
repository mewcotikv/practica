using System;

namespace REDConstruct.Models
{
    public class RaportConsum
    {
        public string Obiectiv { get; set; }
        public string Material { get; set; }
        public decimal Cantitate { get; set; }
        public decimal Cost { get; set; }
        public DateTime DataCalcul { get; set; }
    }
}
