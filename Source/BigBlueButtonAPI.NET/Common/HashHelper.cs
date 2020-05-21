/**
 * Author: dyx1001
 * Email: dyx1001@126.com
 * License: MIT
 * Git URL: https://github.com/dyx1001/BigBlueButtonAPI.NET
 */
using System.Security.Cryptography;
using System.Text;


namespace BigBlueButtonAPI.Common
{
    /// <summary>
    /// The helper class to compute hash.
    /// </summary>
    public class HashHelper
    {
        /// <summary>
        /// Get the hash string using sha1.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string Sha1Hash(string str, string salt =null)
        {
            if (!string.IsNullOrEmpty(salt)) str = str + salt;

            byte[] data = Encoding.UTF8.GetBytes(str);
            
            var sha1 = SHA1.Create();
            var result=sha1.ComputeHash(data);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in result)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }
    }
}