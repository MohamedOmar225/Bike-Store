using System.ComponentModel.DataAnnotations;

namespace bike_store_2.DTO
{
    public class LoginDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }

    }
}
