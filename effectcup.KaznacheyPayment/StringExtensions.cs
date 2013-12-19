using System.Security.Cryptography;
using System.Text;

namespace effectcup.KaznacheyPayment
{
    public static class StringExtensions
    {
        public static string GetMd5Hash(this string input)
        {
            using (var md5Hash = MD5.Create())
            {
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                var sBuilder = new StringBuilder();

                for (var i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
    }
}
