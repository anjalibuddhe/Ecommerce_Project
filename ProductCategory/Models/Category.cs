using System.ComponentModel.DataAnnotations;

namespace ProductCategory.Models
{
    public class Category
    {
        [Key]
        public int CId { get; set; }
        [Required]
        public string? Cname { get; set; }
    }
}
