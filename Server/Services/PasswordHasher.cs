using System;
using System.Linq;
using System.Security.Cryptography;

namespace Server.Services
{
    public class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 10000;

        public string HashPassword(string password, byte[] salt)
        {
            // PBKDF2 (Password-Based Key Derivation Function 2)
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                byte[] saltAndHash = new byte[SaltSize + HashSize];

                Buffer.BlockCopy(salt, 0, saltAndHash, 0, SaltSize);
                Buffer.BlockCopy(hash, 0, saltAndHash, SaltSize, HashSize);

                return Convert.ToBase64String(saltAndHash);
            }
        }

        public bool VerifyPassword(string password, string hashedPassword, byte[] salt)
        {
            byte[] saltAndHash = Convert.FromBase64String(hashedPassword);
            byte[] hash = new byte[HashSize];

            Buffer.BlockCopy(saltAndHash, SaltSize, hash, 0, HashSize);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] computedHash = pbkdf2.GetBytes(HashSize);

                return computedHash.SequenceEqual(hash);
            }
        }

        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[SaltSize];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }
    }
}
