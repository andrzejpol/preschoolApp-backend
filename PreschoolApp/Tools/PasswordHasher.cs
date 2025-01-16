using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using PreschoolApp.Tools.Interfaces;

namespace PreschoolApp.Tools
{
    public class PasswordHasher : IPasswordHasher
    {
        public bool Compare(string password, string hash, byte[] salt)
        {
            return Hash(password, salt) == hash;
        }

        public string Hash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }

        public byte[] RandomSalt(int bytes)
        {
            return RandomNumberGenerator.GetBytes(bytes);
        }
    }
}