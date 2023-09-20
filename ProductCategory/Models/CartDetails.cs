using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Xml;

namespace ProductCategory.Models
{
    public class CartDetails
    {
        [Key]
        public int OrderID { get; set; }
        
        [Required]
        public int UserID { get; set; }
        [Required]
        public int Id { get; set; }

        public int Quantity { get; set; }

    }
}
