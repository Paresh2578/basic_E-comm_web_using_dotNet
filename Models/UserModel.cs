using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace bulkyApp.Models
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MinLength(4)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(10)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        public string? ConformPassword { get; set; }

        [ValidateNever]
        public string? Role { get; set; } = "User";
    }
}
