using System.ComponentModel.DataAnnotations;

namespace bike_store_2.DTO
{
    public class ChangeRolesDto
    {
        [Required(ErrorMessage = "User ID is required")]
        public string? UserId { get; set; }
        [Required(ErrorMessage = "New role is required")]
        public List<string> NewRoles { get; set; } = new List<string>();
        [Required]
        public bool ReplaceOldRoles { get; set; } = true;
    }
}
