using System.ComponentModel.DataAnnotations;

namespace ProductCategory.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int Id { get; set; }
       
        public int Quantity { get; set; }
    }
}
