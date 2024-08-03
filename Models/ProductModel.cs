using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace bulkyApp.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        [Display(Name = "Product Price")]
        [Range(1, 100)]
        [Required]
        [DefaultValue(value: 0)]
        public double ProductPrice { get; set; }


        [Required]
        [Display(Name = "Select cetegory")]
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        [ValidateNever]
        public virtual CategoryModel category { get; set; }

        [ValidateNever]
        public string? ImageUrl { get; set; }
    }
}
