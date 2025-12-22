using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MinLength(3)]
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }= new List<Product>();

    }
}
