using System.ComponentModel.DataAnnotations;

namespace SP23.P02.Web.Features.DTOs
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string? Password { get; set; } = string.Empty;
    }
}
