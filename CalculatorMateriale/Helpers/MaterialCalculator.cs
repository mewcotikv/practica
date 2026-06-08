using System;

namespace CalculatorMateriale.Helpers
{
    public class MaterialCalculator
    {
        // FORMULE SPECIFICE PENTRU TERMOIZOLAȚIE
        public const decimal POLISTIREN_FACTOR = 1.10m;        // suprafata x 1.10
        public const decimal DIBLURI_FACTOR = 6m;               // suprafata x 6
        public const decimal DIBLURI_REZERVA_FACTOR = 1.10m;    // +10% rezerva
        public const decimal ADEZIV_DIVISOR = 6m;               // suprafata / 6
        public const decimal PLASA_FACTOR = 1.15m;              // suprafata x 1.15
        public const decimal TENCUIALA_DIVISOR = 4m;            // suprafata / 4
        public const decimal AMORSA_DIVISOR = 10m;              // suprafata / 10

        // Constante pentru calcule generale
        public const decimal MANOPERA_PERCENT = 35m;            // 35% manoperă
        public const decimal TVA_PERCENT_DEFAULT = 20m;         // 20% TVA

        /// <summary>
        /// Calculeaza consumul total al unui material pe baza suprafetei.
        /// </summary>
        public static decimal CalculateConsumTotal(decimal suprafataM2, decimal consumPeM2)
        {
            return suprafataM2 * consumPeM2;
        }

        /// <summary>
        /// Calculeaza pretul total al unui material pe baza consumului si pretului unitar.
        /// </summary>
        public static decimal CalculatePretTotal(decimal cantitate, decimal pretUnitar)
        {
            return cantitate * pretUnitar;
        }

        /// <summary>
        /// Calculeaza pretul cu TVA (default 20% pentru Moldova).
        /// </summary>
        public static decimal CalculatePretCuTVA(decimal pret, decimal procentTVA = 20)
        {
            return pret * (1 + procentTVA / 100);
        }

        /// <summary>
        /// Calculeaza pretul cu reducere.
        /// </summary>
        public static decimal CalculatePretCuReducere(decimal pret, decimal procentReducere)
        {
            return pret * (1 - procentReducere / 100);
        }

        /// <summary>
        /// Calculeaza necesarul de Polistiren: suprafata x 1.10
        /// </summary>
        public static decimal CalculatePolistiren(decimal suprafataM2)
        {
            return suprafataM2 * POLISTIREN_FACTOR;
        }

        /// <summary>
        /// Calculeaza necesarul de Dibluri: suprafata x 6 + 10% rezerva
        /// </summary>
        public static decimal CalculateDibluri(decimal suprafataM2)
        {
            return Math.Ceiling(suprafataM2 * DIBLURI_FACTOR * DIBLURI_REZERVA_FACTOR);
        }

        /// <summary>
        /// Calculeaza necesarul de Adeziv: suprafata / 6
        /// </summary>
        public static decimal CalculateAdeziv(decimal suprafataM2)
        {
            return Math.Ceiling(suprafataM2 / ADEZIV_DIVISOR);
        }

        /// <summary>
        /// Calculeaza necesarul de Plasa: suprafata x 1.15
        /// </summary>
        public static decimal CalculatePlasa(decimal suprafataM2)
        {
            return suprafataM2 * PLASA_FACTOR;
        }

        /// <summary>
        /// Calculeaza necesarul de Tencuiala: suprafata / 4
        /// </summary>
        public static decimal CalculateTencuiala(decimal suprafataM2)
        {
            return Math.Ceiling(suprafataM2 / TENCUIALA_DIVISOR);
        }

        /// <summary>
        /// Calculeaza necesarul de Amorsa: suprafata / 10
        /// </summary>
        public static decimal CalculateAmorsa(decimal suprafataM2)
        {
            return Math.Ceiling(suprafataM2 / AMORSA_DIVISOR);
        }

        /// <summary>
        /// Calculeaza consumul specific pe baza tipului de material
        /// </summary>
        public static decimal CalculateConsumByType(string tipMaterial, decimal suprafataM2)
        {
            return tipMaterial?.ToLower() switch
            {
                "polistiren" => CalculatePolistiren(suprafataM2),
                "dibluri" => CalculateDibluri(suprafataM2),
                "adeziv" => CalculateAdeziv(suprafataM2),
                "plasa" => CalculatePlasa(suprafataM2),
                "tencuiala" => CalculateTencuiala(suprafataM2),
                "amorsa" => CalculateAmorsa(suprafataM2),
                _ => suprafataM2
            };
        }

        /// <summary>
        /// Calculeaza manopera ca procent din totalul materialelor (35%)
        /// </summary>
        public static decimal CalculateManopera(decimal valoareMateriale)
        {
            return valoareMateriale * (MANOPERA_PERCENT / 100);
        }

        /// <summary>
        /// Calculeaza totalul cu manopera si TVA (20%)
        /// </summary>
        public static decimal CalculateDevizTotal(decimal valoareMateriale, bool adaugaManopera = true, decimal procentTVA = TVA_PERCENT_DEFAULT)
        {
            var total = valoareMateriale;
            
            if (adaugaManopera)
            {
                total += CalculateManopera(valoareMateriale);
            }

            return CalculatePretCuTVA(total, procentTVA);
        }

        /// <summary>
        /// Calculeaza totalul cu detalii complete pentru deviz
        /// </summary>
        public static (decimal MaterialeTotal, decimal Manopera, decimal Subtotal, decimal TVA, decimal Total) 
            CalculateDevizDetailed(decimal valoareMateriale, decimal procentTVA = TVA_PERCENT_DEFAULT)
        {
            var manopera = CalculateManopera(valoareMateriale);
            var subtotal = valoareMateriale + manopera;
            var tva = subtotal * (procentTVA / 100);
            var total = subtotal + tva;

            return (valoareMateriale, manopera, subtotal, tva, total);
        }

        /// <summary>
        /// Calculeaza necesarul de adeziv in functie de suprafata si consumul pe m2.
        /// </summary>
        public static decimal CalculateAdezivNecesar(decimal suprafataM2, decimal consumAdezivPeM2 = 5)
        {
            return suprafataM2 * consumAdezivPeM2;
        }

        /// <summary>
        /// Calculeaza necesarul de dibluri pe baza suprafetei.
        /// </summary>
        public static int CalculateDibluriNecesari(decimal suprafataM2, int dibluriPeM2 = 4)
        {
            return (int)Math.Ceiling(suprafataM2 * dibluriPeM2);
        }

        /// <summary>
        /// Calculeaza necesarul de plasa de fibra de sticla.
        /// </summary>
        public static decimal CalculatePlasaNecesara(decimal suprafataM2, decimal suprapunereProcent = 0.1m)
        {
            return suprafataM2 * (1 + suprapunereProcent);
        }
    }
}
