using System.ComponentModel.DataAnnotations;
using TV_Domain;

namespace Homework_TV_MVC.Models
{
    public class TVShowModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string URL { get; set; }

        [Required]
        public Rating.ERating Rating { get; set; }
    }
}