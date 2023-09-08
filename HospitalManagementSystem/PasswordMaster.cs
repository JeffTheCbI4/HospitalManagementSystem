using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    internal class PasswordMaster
    {
        public static string GenerateSalt()
        {
            string symbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder salt = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < 30; i++)
            {
                int r = rand.Next(0, symbols.Length);
                salt.Append(symbols[r]);
            }
            return salt.ToString();
        }

        public static string GetSaltHashedPassword(string password, string salt)
        {
            string saltPassword = password + salt;
            SHA256 sha = SHA256.Create();
            byte[] byteArray = Encoding.Unicode.GetBytes(saltPassword);
            byte[] hash = sha.ComputeHash(byteArray);
            StringBuilder stringHash = new StringBuilder();
            foreach (byte b in hash)
            {
                stringHash.Append(b.ToString());
            }
            return stringHash.ToString();
        }

        public static bool VerifyPassword(string password, string passwordHash, string salt)
        {
            string newHashPassword = GetSaltHashedPassword(password, salt);
            return newHashPassword.Equals(passwordHash);
        }
    }
}
