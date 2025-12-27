namespace SecureVault.Models
{
    public class LoginModel
    {
        public string Username { get; set; }  // Optional if using email only
        public string Email { get; set; }     // Optional if using username only
        public string Password { get; set; }
    }
}