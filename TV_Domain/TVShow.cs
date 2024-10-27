using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TV_Domain.Rating;

namespace TV_Domain
{
    public class TVShow
    {
        public TVShow()
        {
            Id = new Guid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public ERating Rating { get; set; }

        public bool IsDeleted { get; set; } = false;
        public string URL { get; set; }

        [ForeignKey("AttachmentId")]
        public Guid AttachmentId { get; set; }

        public Attachment Attachment { get; set; }
        public virtual ICollection<TVShowLanguages> TVShowLanguages { get; set; }
    }
}