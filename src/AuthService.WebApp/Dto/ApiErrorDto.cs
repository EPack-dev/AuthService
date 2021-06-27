using System.ComponentModel.DataAnnotations;

namespace AuthService.WebApp.Dto
{
    public class ApiErrorDto
    {
        [Required]
        public string? Message { get; set; }
    }
}
