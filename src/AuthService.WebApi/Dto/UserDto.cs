using System;
using AuthService.Model;

namespace AuthService.WebApi.Dto
{
    public class UserDto
    {
        public UserDto(User model)
        {
            Login = model.Login;
            Role = model.Role;
            Created = model.Created;
        }

        public string Login { get; set; }

        public UserRole Role { get; set; }

        public DateTime Created { get; set; }
    }
}
