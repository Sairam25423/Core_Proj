using System.ComponentModel.DataAnnotations;

namespace SixthDayTsk.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required.")]
        [StringLength(100, ErrorMessage = "Max Length is 100 Characters.")]
        [MinLength(3, ErrorMessage = "Name must contains Min 3 Characters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name can contain alphabets and spaces only.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Eamil is Reauired.")]
        [StringLength(100, ErrorMessage = "Max Length is 100 Characters.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Range(18, 100, ErrorMessage = "Age must be minimum 18 and max 100.")]
        public int Age { get; set; }
        public bool IsActiveUser { get; set; } = true;
    }
}
