namespace CRYPT
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class MD5Core
    {
        public static string GetMd5Hash(string input)
        {
            if (input == null)
            {
                return null;
            }
            byte[] buffer = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public static byte[] GetMd5Hash(byte[] input)
        {
            if (input == null)
            {
                return null;
            }
            return MD5.Create().ComputeHash(input);
        }
    }
}

