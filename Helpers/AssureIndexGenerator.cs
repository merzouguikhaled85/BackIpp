namespace WebApplication1.Helpers
{
    public class AssureIndexGenerator
    {
        private static Random randomIndex = new Random();
        private const int IndexLength = 12;
        private const string Chars = "0123456789";

        public static string GenerateIndex()
        {
            char[] indexAssure = new char[IndexLength];

            for (int i = 0; i < IndexLength; i++)
            {
                indexAssure[i] = Chars[randomIndex.Next(Chars.Length)];
            }

            return new string(indexAssure);
        }
    }
}
