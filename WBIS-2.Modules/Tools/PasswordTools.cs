using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace WBIS_2.Modules.Tools
{
    public static class PasswordTools
    {
        public static string HashPassword(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
            sb.Append(b.ToString("X2"));
                }

                return sb.ToString().ToLower();
            }
        }
        //public static string EncodePasswordToBase64(string password)
        //{
        //    byte[] encData_byte = new byte[password.Length];
        //    encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
        //    string encodedData = Convert.ToBase64String(encData_byte);
        //    return encodedData;
        //}
        //public static string DecodePasswordToBase64(string encodedData)
        //{
        //    System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        //    System.Text.Decoder utf8Decode = encoder.GetDecoder();
        //    byte[] todecode_byte = Convert.FromBase64String(encodedData);
        //    int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        //    char[] decoded_char = new char[charCount];
        //    utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        //    string result = new String(decoded_char);
        //    return result;
        //}
    }
}
