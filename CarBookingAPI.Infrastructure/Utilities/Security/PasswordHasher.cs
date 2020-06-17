using CarBookingAPI.Core.Contracts.Utilities;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace CarBookingAPI.Infrastructure.Utilities.Security
{
    public class PasswordHasher : IHasher
    {
        private string _secret;

        public PasswordHasher(string secret)
        {
            _secret = secret;
        }

        public PasswordHasher()
        {

        }

        public byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }

        public string HashPassword(string password, byte[] salt)
        {
            var argon = new Argon2id(Encoding.Default.GetBytes(password));
            argon.KnownSecret = Encoding.Default.GetBytes(_secret);
            argon.Salt = salt;
            argon.DegreeOfParallelism = 1;
            argon.Iterations = 4;
            argon.MemorySize = 1024 * 4;

            return Encoding.UTF8.GetString(argon.GetBytes(16));
        }
    }
}
