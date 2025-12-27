using System.ComponentModel.DataAnnotations;
namespace SecureVault.Models
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }
    }
}