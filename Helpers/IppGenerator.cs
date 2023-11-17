namespace WebApplication1.Helpers
{
    public class IppGenerator
    {
        private static Random random = new Random();

        public static string GenerateIpp()
        {
            const string indicatif = "71";
            const int ippLength = 8;  // longueur de ipp sur 10 chiffres :2chiffres pour code hmpit 71
            const string chars = "0123456789"; // Vous devez inclure tous les chiffres de 0 à 9.

            char[] ipp = new char[ippLength];
            for (int i = 0; i < ippLength; i++)
            {
                ipp[i] = chars[random.Next(chars.Length)];
            }

            return new string(indicatif) + new string(ipp); //on ajoute l'indicatif 71 => HMPIT
        }

    }
}
