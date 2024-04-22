using System.ComponentModel.DataAnnotations;

namespace ModelSharingService.DTO
{
    public class RegistrationRequestResponseDTO : AuthResponseDTO
    {
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        [Required]
        public string password { get; set; } = string.Empty;
    }
}
