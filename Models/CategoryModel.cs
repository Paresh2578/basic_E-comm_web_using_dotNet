using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bulkyApp.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Enter Category")]
        public string CategoryName { get; set; }
    }
}
