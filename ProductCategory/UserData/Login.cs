using System.ComponentModel.DataAnnotations;

namespace ProductCategory.UserData
{
    public class Login
    {
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(30, ErrorMessage = "Email address  Does not match")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
