using System.ComponentModel.DataAnnotations;

namespace AuthService.WebApp.Dto
{
    public class AccountDto
    {
        [Required]
        public string? Login { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
