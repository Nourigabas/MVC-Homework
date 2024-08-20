using System.ComponentModel.DataAnnotations;

namespace Homework_TV_MVC.Models
{
    public class LanguagesModel
    {
        [Required]
        public required List<string> Languages { get; set; }

    }
}
