using System.ComponentModel.DataAnnotations;

namespace ModelSharingService.DTO
{
    public class UserLoginRequestResponseDTO : AuthResponseDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
