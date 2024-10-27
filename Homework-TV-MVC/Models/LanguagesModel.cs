using System.ComponentModel.DataAnnotations;

namespace Homework_TV_MVC.Models
{
    public class LanguagesModel
    {
        [Required]
        public List<string> Languages { get; set; }
    }
}