using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EjercicioPOO.API.Helper
{
    public static class HashHelper
    {
        public static HashedPassword HashPasword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return new HashedPassword { Password = hash, Salt = Convert.ToBase64String(salt) };
        }

        public static bool VerifyPassword(string password, string hash, string salt)
        {
            var hashPass = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashPass == hash;
        }

        public class HashedPassword
        {
            public string Password { get; set; }
            public string Salt { get; set; }
        }
    }
}
