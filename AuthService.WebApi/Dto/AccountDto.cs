using System.ComponentModel.DataAnnotations;

namespace AuthService.WebApi.Dto
{
    public class AccountDto
    {
        [Required]
        public string? Login { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
