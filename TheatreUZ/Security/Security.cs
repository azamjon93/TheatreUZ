using System.Security.Cryptography;
using System.Text;

namespace TheatreUZ
{
    public static class OwnSecurity
    {
        public static string Hash(string text)
        {
            var crypt = new SHA256Managed();
            string hash = string.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(text));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }

            return hash;
        }
    }
}