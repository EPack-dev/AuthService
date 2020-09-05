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

        public string Login { get; }

        public UserRole Role { get; }

        public DateTime Created { get; }
    }
}
