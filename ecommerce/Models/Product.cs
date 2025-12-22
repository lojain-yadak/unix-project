using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "product name is required.")]
        [MinLength(3,ErrorMessage ="product name must be at least 3 characters")]
        [MaxLength(100,ErrorMessage ="product name can't axceed 100 character")]

        public string Name { get; set; }
        [Required(ErrorMessage ="product description is required.") ]
        [MaxLength(20, ErrorMessage = "product description can't axceed 20 character")]

        public string Description { get; set; }
        [Required(ErrorMessage = "product price is required.")]
        [Range(1,10000,ErrorMessage ="product price must be between 1 & 10000")]
        public decimal Price { get; set; }
        [Range(1,5)]

        public int? Rate { get; set; }
        [Required(ErrorMessage = "product quantity is required.")]
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
        [ValidateNever]
        public string? Image { get; set; }
        public int CategoryId {get; set;}
        [ValidateNever]
        public Category Category { get; set;}

    }
}
