using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public required string Name { get; set; }
        public required string Path { get; set; }
        public required string FileType { get; set; }
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("TVShowId")]
        public Guid TVShowId { get; set; }
        public TVShow TVShow { get; set; }
    }
}
