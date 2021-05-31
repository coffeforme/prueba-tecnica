using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace core.util
{
    public class PBKDF2
    {
        public class PBKDFPassword
        {
            public byte[] password { get; set; }
            public byte[] salt { get; set; }
        }

        public static bool PasswordValid(string passwordToHash, byte[] pass, byte[] salt)
        {
            var hashed = HashPassword(passwordToHash, salt);
            return StructuralComparisons.StructuralEqualityComparer.Equals(pass, hashed.password);
        }

        public static PBKDFPassword HashPassword(string passwordToHash, byte[] salt = null, int numberOfRounds = 100000)
        {
            PBKDFPassword ok = new PBKDFPassword();
            ok.salt = salt ?? GenerateSalt();
            ok.password = HashPassword(Encoding.UTF8.GetBytes(passwordToHash), ok.salt, numberOfRounds);
            return ok;
        }

        private static byte[] GenerateSalt()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }

        private static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds))
            {
                return rfc2898DeriveBytes.GetBytes(32);
            }
        }

        /// <summary>
        /// Compare password hashes for equality. Uses a constant time comparison method.
        /// </summary>
        /// <param name="passwordHash1"></param>
        /// <param name="passwordHash2"></param>
        /// <returns></returns>
        public static bool Compare(string passwordHash1, string passwordHash2)
        {
            if (passwordHash1 == null || passwordHash2 == null)
                return false;

            int min_length = Math.Min(passwordHash1.Length, passwordHash2.Length);
            int result = 0;

            for (int i = 0; i < min_length; i++)
                result |= passwordHash1[i] ^ passwordHash2[i];

            return 0 == result;
        }

    }
}
