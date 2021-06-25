using System;

namespace AuthService.Model
{
    public class User
    {
        public User(Account account)
        {
            Login = account.Login;
            Role = account.Role;
            Created = account.Created;
        }

        public User(Account account, string token)
            : this(account)
        {
            Token = token;
        }

        public string Login { get; }

        public UserRole Role { get; }

        public DateTime Created { get; }

        public string? Token { get; }
    }
}
