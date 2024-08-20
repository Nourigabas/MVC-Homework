using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public required string Title { get; set; }
        [Required]
        public required DateTime ReleaseDate { get; set; }
        [Required]
        public required ERating Rating { get; set; }
        public bool IsDeleted { get; set; } = false;
        public required string URL { get; set; }
        [ForeignKey("AttachmentId")]
        public Guid AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
        public virtual ICollection<TVShowLanguages> TVShowLanguages { get; set; }
    }
}
