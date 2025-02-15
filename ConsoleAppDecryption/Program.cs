﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleAppDecryption
{
    class Program
    {
        static void Main(string[] args)
        {


            string str_passwordSavedInDBEncrypted = "y1lHkpHdPXK+/upgdyIaPA==";
            if ("test" == Decrypt(str_passwordSavedInDBEncrypted, "tk&quot;9&amp;:z_pR#C&lt;jkb"))
            {
                Console.WriteLine("Decryption Success.. working fine!");
            }
        }

        public static string Decrypt(string cipherText, string EncryptionKey)
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                //encryptor.Padding = PaddingMode.None;

                //MemoryStream ms = new MemoryStream();
                //CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write);
                //cs.Write(cipherBytes, 0, cipherBytes.Length);
                //cs.FlushFinalBlock();

                //cipherText = Encoding.Unicode.GetString(ms.ToArray());
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);

                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }
}
