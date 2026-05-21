using System;

namespace CalculatorMateriale.Helpers
{
    public class MaterialCalculator
    {
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
        /// Calculeaza pretul cu TVA.
        /// </summary>
        public static decimal CalculatePretCuTVA(decimal pret, decimal procentTVA = 19)
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
