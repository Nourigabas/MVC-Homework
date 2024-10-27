using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV_Domain
{
    public class Attachment
    {
        public Attachment()
        {
            Id = new Guid();
        }

        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }
        public string FileType { get; set; }
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("TVShowId")]
        public Guid TVShowId { get; set; }

        public TVShow TVShow { get; set; }
    }
}