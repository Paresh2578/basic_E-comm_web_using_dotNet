using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bulkyApp.Models
{
    public class CardModel
    {

        [Key]
        public int CardID { get; set; }

        [Required]
        public int Quentity { get; set; }

        [Required]
        public int ProuctID { get; set; }
        [ForeignKey("ProuctID")]
        public virtual ProductModel Product { get; set; }

        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual UserModel User { get; set; }
    }
}
