using System.ComponentModel.DataAnnotations;

namespace bike_store_2.DTO
{
    public class RegisterUserDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
        public string? Email { get; set; }

    }
}
