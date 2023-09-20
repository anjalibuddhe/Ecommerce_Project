using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCategory.Models
{
    public class Product
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { set; get; }
        [Required]
        public string? Name { get; set; }

        [Required]
        public double Price { get; set; }

        public string? Imageurl { get; set; }
        [Required]
        [Display(Name = "Cate Name")]

        public int CId { get; set; }

        [Display(Name = "Cate Name")]
        public string? Cname { get; set; }
        [NotMapped]
        public int Quantity { get; set; }
        [NotMapped]
        public int CartId { get; set; }
        [NotMapped]

        
        public DateTime dateTime { get; set; }
    }
}