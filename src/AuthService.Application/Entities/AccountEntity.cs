using System;
using AuthService.Model;

namespace AuthService.Application.Entities
{
    public record AccountEntity(
        string Login,
        UserRole Role,
        DateTime Created,
        byte[] PasswordHash,
        byte[] PasswordSalt);
}
