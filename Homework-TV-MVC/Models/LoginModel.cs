using System.ComponentModel.DataAnnotations;

namespace Homework_TV_MVC.Models
{
    public class LoginModel
    {
        [EmailAddress]
        [Required]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}