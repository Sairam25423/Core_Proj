using System.ComponentModel.DataAnnotations;

namespace DayThreeVal.Models
{
    public class User
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Range(18, 60, ErrorMessage = "Age must be between 18 and 60.")]
        public int Age { get; set; }
    }
}
