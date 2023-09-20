using System.ComponentModel.DataAnnotations;


namespace ProductCategory.UserData
{
    public class Register
    {

        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(25, ErrorMessage = "First name cannot exceed 15 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(25, ErrorMessage = "Last name cannot exceed 15 characters.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        [StringLength(15, ErrorMessage = "Contact number cannot exceed 12 characters.")]
        public string? Contact_No { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(25, ErrorMessage = "Email address cannot exceed 30 characters.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(25)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }


     
        public int RoleId { get; set; }


    }

}

