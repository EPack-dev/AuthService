using System.ComponentModel.DataAnnotations;

namespace AuthService.WebApi.Dto
{
    public class ApiErrorDto
    {
        [Required]
        public string? Message { get; set; }
    }
}
