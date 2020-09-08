using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;

namespace AuthService.Model
{
    public class Account
    {
        public Account(string login, string password, UserRole role)
        {
            ValidateLogin(login);
            ValidatePassword(password);

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

        private void ValidateLogin(string login)
        {
            if (login.Length < 3)
            {
                throw new ServiceException("Invalid login.", HttpStatusCode.BadRequest);
            }
        }

        private void ValidatePassword(string password)
        {
            if (password.Length < 6)
            {
                throw new ServiceException("Invalid password.", HttpStatusCode.BadRequest);
            }
        }
    }
}
