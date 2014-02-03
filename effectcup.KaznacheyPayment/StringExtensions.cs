using System.Security.Cryptography;
using System.Text;

namespace effectcup.KaznacheyPayment
{
    public static class StringExtensions
    {
        /// <summary>
        /// Compute Md5 hash code
        /// </summary>
        /// <param name="input">Source string</param>
        /// <returns>Md5 in string</returns>
        public static string GetMd5Hash(this string input)
        {
            using (var md5Hash = MD5.Create())
            {
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sBuilder = new StringBuilder();
                foreach (byte b in data)
                {
                    sBuilder.Append(b.ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
    }
}
