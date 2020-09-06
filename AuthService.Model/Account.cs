using System;
using System.Linq;
using System.Security.Cryptography;

namespace AuthService.Model
{
    public class Account
    {
        public Account(string login, string password, UserRole role)
        {
            Login = login;
            Role = role;
            Created = DateTime.UtcNow;
            HashPassword(password);
        }

        public string Login { get; private set; }

        public UserRole Role { get; private set; }

        public DateTime Created { get; private set; }

        public byte[] PasswordHash { get; private set; } = default!;

        public byte[] PasswordSalt { get; private set; } = default!;

        public bool VerifyPassword(string password)
        {
            using var hmac = new HMACSHA512(PasswordSalt);
            byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(PasswordHash);
        }

        private void HashPassword(string password)
        {
            using var hmac = new HMACSHA512();
            PasswordSalt = hmac.Key;
            PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
