using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using CommonSettingsHandling;
namespace NoLogin
{
    class Hashing
    {
        
        public static string GetHash(string pass)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(pass, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }
        public static void Verify(string Username,string Password)
        {
            string savedPasswordHash = GetUserHash(Username);
            if(savedPasswordHash=="not found")
            {
                throw new FileNotFoundException();
            }
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(Password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    throw new UnauthorizedAccessException();
        }
        public static void Store(string Username, string Password)
        {
            
            Settings.Set("Hash."+Username,GetHash(Password));
            Settings.Save();
            
        }
        public static string GetUserHash(string Username)
        {
            
            string hash = "";
            try { hash = Settings.Variables["Hash."+Username]; } catch { hash = "not found"; }
            if (hash == "") { hash = "not found"; }
            return hash;
        }
        
    }
}
