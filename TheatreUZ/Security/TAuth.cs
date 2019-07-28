using System.Security.Cryptography;
using System.Text;
using System.Web;
using TheatreUZ.Models;

namespace TheatreUZ.Security
{
    public static class TAuth
    {
        public static bool IsLogged()
        {
            return GetUser() == null ? false : true;
        }

        public static bool IsAdmin()
        {
            var user = GetUser();

            return (user == null || user.Role.Name != "Admin") ? false : true;
        }

        static User GetUser()
        {
            User user = (User)HttpContext.Current.Session["User"];

            return user;
        }

        public static string Hash(string text)
        {
            SHA256Managed crypt = new SHA256Managed();
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