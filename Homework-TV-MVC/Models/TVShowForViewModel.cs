using static TV_Domain.Rating;

namespace Homework_TV_MVC.Models
{
    public class TVShowForViewModel
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public required DateTime ReleaseDate { get; set; }
        public required ERating Rating { get; set; }
        public required string URL { get; set; }
    }
}
