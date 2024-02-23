using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace DefineFIT.Domain.Helpers
{
    public static class PasswordHashHelper
    {
        public static Byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public static string GenerateHashPassword(string password, byte[] salt)
        {
            string hashPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashPassword;
        }   
        
        public static bool VerifyPassword(string password, byte[] salt, string hashPassword)
        {
            string newHashPassword = GenerateHashPassword(password, salt);
            return newHashPassword == hashPassword;
        }
    }
}
