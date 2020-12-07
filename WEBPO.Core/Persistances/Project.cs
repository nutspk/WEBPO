using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using WEBPO.Core.Interfaces;
using WEBPO.Core.Services;
using WEBPO.Domain.Data;
using WEBPO.Domain.Entities;

namespace WEBPO.Core.Persistances
{
    public static class Project
    {
        private static string PrivateKey = "c3VwYWtpdCBzcml3YXRjaGFyYW1ldGhlZQ";
        private static MS_USER user;
        public static void ReadConfiguration() {
            
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=(local); Database=EDI;User Id=sqluser;password=sqluser; Trusted_Connection=false; MultipleActiveResultSets=true");

            AppDbContext dbContext = new AppDbContext(optionsBuilder.Options);

            using (AppDbContext context = new AppDbContext(optionsBuilder.Options))
            {
                user = context.MsUser.Find("admin");
            }
        }

        public static MS_USER GetUser() {
            return user;
        }


        public static string Encrypt(string txt)
        {
            string Hash = "";
            try
            {
                byte[] data = UTF8Encoding.UTF8.GetBytes(txt);

                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(""));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() 
                            { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        Hash = Convert.ToBase64String(results, 0, results.Length);
                    }
                }
            } catch (Exception) {
                Hash = "";
            }
            return Hash;
        }

        public static bool Decrypt(string txt, string Hash)
        {
            string result = "";
            string ActualHash = "";
            try
            {
                byte[] data = UTF8Encoding.UTF8.GetBytes(txt);
                ActualHash = Encrypt(txt);

                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(PrivateKey));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        result = Convert.ToBase64String(results, 0, results.Length);
                    }
                }
            } catch (Exception ex) {
                result = "";
                return false;
            }
            return (ActualHash == Hash);
        }

        public static string EncodeBase64(string text)
        {
            if (string.IsNullOrEmpty(text)) return null;
            byte[] textAsBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textAsBytes);
        }

        public static string DecodeBase64(string encodedText)
        {
            if (string.IsNullOrEmpty(encodedText)) return null;

            byte[] textAsBytes = System.Convert.FromBase64String(encodedText);
            return Encoding.UTF8.GetString(textAsBytes);
        }

    }


    public static class ROLE
    {
        public const string Admin = "ADMIN";
        public const string Domes_User = "USER";
        public const string Overz_User = "USER";
        public const string User = "USER";
    }

    public static class ROLE_ID
    {
        public const string Admin = "99";
        public const string Domes_User = "00";
        public const string Overz_User = "01";
    }

    public static class VENDOR_TYPE
    {
        public const string EDI = "EDI";
        public const string NonEDI = "NON EDI";
    }

    public static class VENDOR_TYPE_ID
    {
        public const string EDI = "01";
        public const string NonEDI = "00";
    }

    public class EmailConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; } = true;
        public string DisplayName { get; set; }
        public bool UseDefaultCredentials { get; set; } = true;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string IncludeTestEMail { get; set; }
    }

}
