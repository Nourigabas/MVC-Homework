using System.ComponentModel.DataAnnotations;
using TV_Domain;
using static TV_Domain.Rating;

namespace Homework_TV_MVC.Models
{
    public class TVShowModel
    {
        [Required]
        public required string Title { get; set; }
        [Required]
        public required DateTime ReleaseDate { get; set; }
        [Required]
        public required string URL { get; set; }
        [Required]
        public Rating.ERating Rating { get; set; }

    }
}
