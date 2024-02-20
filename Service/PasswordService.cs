using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace FinancialManagement.Service
{
    public class PasswordService
    {
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));

            string saltString = Convert.ToBase64String(salt);
            string saltedHashedPassword = $"{saltString}:{hashedPassword}";

            return saltedHashedPassword;
        }

        public static bool VerifyPassword(string password, string saltedHashedPassword)
        {
            string[] parts = saltedHashedPassword.Split(':');
            if (parts.Length != 2)
            {
                return false;
            }

            string saltString = parts[0];
            string hashedPassword = parts[1];
            byte[] salt = Convert.FromBase64String(saltString);

            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}