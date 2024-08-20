using System.ComponentModel.DataAnnotations;

namespace Homework_TV_MVC.Models
{
    public class LoginModel
    {
        [EmailAddress]
        [Required]
        public required string Username { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public required string Password { get; set; }
    }
}
