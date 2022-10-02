using System.Text;

namespace DAL
{
    public class SHA1
    {
        public static string SHA1Cr(string input)
        {
            using (var provider = System.Security.Cryptography.SHA1.Create())
            {
                StringBuilder builder = new StringBuilder();

                foreach (Byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(input)))
                    builder.Append(b.ToString("x2").ToLower());

                return builder.ToString();
            }
        }
    }
}
