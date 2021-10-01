using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.IO;

namespace Registration.EncryptDecryptProcessor
{
    public class PasswordProcessing
    {
        public readonly IConfiguration _configuration;

        /*public PasswordProcessing()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build(); ;
        }*/

        /// <summary>
        /// Declaration of encrypt method
        /// </summary>
        /// <param name="originalString">Passing password string</param>
        /// <returns>return string</returns>
        public static string Encrypt(string originalString, string key)
        {
            //string Key = _configuration["SecurityKey"];
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes(key);

            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException("The string which needs to be encrypted can not be null.");
            }
            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            var writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        /// <summary>
        /// Declaration of decrypt method
        /// </summary>
        /// <param name="encryptedString">passing string</param>
        /// <returns>return string</returns>
        public static string Decrypt(string encryptedString, string key)
        {
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes(key);
            if (String.IsNullOrEmpty(encryptedString))
            {
                throw new ArgumentNullException("The string which needs to be decrypted can not be null.");
            }

            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream(Convert.FromBase64String(encryptedString));
            var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes),CryptoStreamMode.Read);
            var reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }



        //=================   Simple logic =============================


        //public static string key = "CoolPassword";
        //public static string PasswordEncrypt(string password)
        //{
        //    if (string.IsNullOrEmpty(password)) return "";
        //    var passwordBytes = Encoding.UTF8.GetBytes(password);
        //    var result1 = (Convert.ToBase64String(passwordBytes));
        //    //var re= (result1.Remove( result1.Length-1 ));
        //    return result1;

        // }
        //public static string PasswordDecrypt(string Base64EncodedData)
        //{ 
        //    if (string.IsNullOrEmpty(Base64EncodedData)) return "";
        //    var Base64EncodedByte = Convert.FromBase64String(Base64EncodedData);
        //    var result = Encoding.UTF8.GetString(Base64EncodedByte);
        //   // result = result.Substring(1, result.Length - key.Length);
        //    return result;
        //}
    }
}
