using static TV_Domain.Rating;

namespace Homework_TV_MVC.Models
{
    public class TVShowForViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ERating Rating { get; set; }
        public string URL { get; set; }
    }
}