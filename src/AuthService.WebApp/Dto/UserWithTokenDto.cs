using AuthService.Model;

namespace AuthService.WebApp.Dto
{
    public class UserWithTokenDto : UserDto
    {
        public UserWithTokenDto(User model)
            : base(model)
        {
            Token = model.Token;
        }

        public string? Token { get; set; }
    }
}
